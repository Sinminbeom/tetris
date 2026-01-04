using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Google.Protobuf;
using Google.Protobuf.Protocol;

public enum EServerType
{
    GameServer = 0,
}

public class ServerInstance
{
	ServerSession _session = null;
	Connector _connector = new Connector();
	long _lastHeartbeatTick = 0;
	const int LOBBY_HEARTBEAT_MS = 20000; // 20s
	const int INROOM_HEARTBEAT_MS = 5000; // 5s


	public bool IsConnected()
	{
		if (_session == null)
			return false;

		return _session.IsConnected();
	}

	public void Send(IMessage packet)
	{
		if (_session != null)
			_session.Send(packet);
	}

	public void Connect(IPEndPoint endPoint, Action onSuccessCallback = null, Action onFailedCallback = null)
	{
		_session = new ServerSession();
		_lastHeartbeatTick = 0;
		_connector.OnSuccessCallback = () => { PushAction(onSuccessCallback); _connector.OnSuccessCallback = null; };
		_connector.OnFailedCallback = () => { PushAction(onFailedCallback); _connector.OnFailedCallback = null; };
		_connector.Connect(endPoint, () => { return _session; });

	}

	public void Update()
	{
		ExecuteAction();

		if (_session == null)
			return;

		List<PacketMessage> list = PacketQueue.Instance.PopAll(_session);
		foreach (PacketMessage packet in list)
		{
			Action<PacketSession, IMessage> handler = PacketManager.Instance.GetPacketHandler(packet.Id);
			if (handler != null)
				handler.Invoke(_session, packet.Message);
		}
		SendHeartbeatIfNeeded();
	}

	void SendHeartbeatIfNeeded()
	{
		if (_session == null || !_session.IsConnected())
			return;

		int interval = (Managers.Room != null && Managers.Room.RoomInfo != null) ? INROOM_HEARTBEAT_MS : LOBBY_HEARTBEAT_MS;
		long now = Environment.TickCount;
		if (now - _lastHeartbeatTick >= interval)
		{
			_session.Send(new C_Ping());
			_lastHeartbeatTick = now;
		}
	}


	public void Disconnect()
	{
		if (_session != null)
			_session.Disconnect();

		_session = null;
		_lastHeartbeatTick = 0;
	}

	#region ActionQueue
	object _lock = new object();
	Queue<Action> _actionQueue = new Queue<Action>();

	void PushAction(Action action)
	{
		lock (_lock)
		{
			_actionQueue.Enqueue(action);
        }
	}

	void ExecuteAction()
	{
		if (_actionQueue.Count == 0)
			return;

		lock (_lock)
		{
			while (_actionQueue.TryDequeue(out Action action))
			{
				action?.Invoke();
			}
		}
	}
	#endregion
}

public class NetworkManager
{
    public ServerInstance GameServer { get; } = new ServerInstance();

    // 마지막으로 성공/시도했던 서버 정보 저장 (재연결에 필요)
    private IPEndPoint _lastEndPoint;
    private Action _onConnectSuccess;
    private Action _onConnectFailed;

    // 재연결 중복 방지
    private bool _isConnectingOrReconnecting;

    // Managers(모노)에서 코루틴을 돌릴 수 있게 참조용
    private MonoBehaviour _runner;

    public void SetRunner(MonoBehaviour runner)
    {
        _runner = runner;
    }

	// 기존 Connect 호출부를 이 함수로 통일해두면 재연결이 쉬워집니다.
	public void ConnectToGameServer(IPEndPoint endPoint, Action onSuccess, Action onFailed)
	{
		_lastEndPoint = endPoint;
		_onConnectSuccess = onSuccess;
		_onConnectFailed = onFailed;

		if (GameServer != null && GameServer.IsConnected())
			return;

		if (_isConnectingOrReconnecting)
			return;

        _isConnectingOrReconnecting = true;

        GameServer.Connect(endPoint,
            () =>
            {
                _isConnectingOrReconnecting = false;
                onSuccess?.Invoke();
            },
            () =>
            {
                _isConnectingOrReconnecting = false;
                onFailed?.Invoke();
            });
    }

    // 포그라운드 복귀 시 호출할 자동 재연결 시작점
    public void TryAutoReconnect()
    {
        if (_runner == null)
        {
            Debug.LogWarning("NetworkManager runner is not set. Call Managers.Network.SetRunner(this) from Managers.");
            return;
        }

        if (GameServer != null && GameServer.IsConnected())
            return;

        if (_isConnectingOrReconnecting)
            return;

        if (_lastEndPoint == null)
        {
            Debug.LogWarning("No last endpoint to reconnect to.");
            return;
        }

        _runner.StartCoroutine(CoReconnect());
    }

	private System.Collections.IEnumerator CoReconnect()
	{
		_isConnectingOrReconnecting = true;

		float delay = 0.5f;      // 초기 재시도 대기
		float maxDelay = 5.0f;   // 최대 대기
		int maxTries = 10;       // 최대 재시도 횟수 (원하면 조정)

		for (int i = 0; i < maxTries; i++)
		{
			if (GameServer != null && GameServer.IsConnected())
			{
				_isConnectingOrReconnecting = false;
				yield break;
			}

			Debug.Log($"[Reconnect] Try {i + 1}/{maxTries} in {delay:0.0}s...");

			yield return new WaitForSeconds(delay);

			// 재시도 직전에 한 번 더 체크
			if (GameServer != null && GameServer.IsConnected())
			{
				_isConnectingOrReconnecting = false;
				yield break;
			}

            // 실제 Connect 시도
            bool connectDone = false;
            bool connectOk = false;

            GameServer.Connect(_lastEndPoint,
                () => { connectDone = true; connectOk = true; },
                () => { connectDone = true; connectOk = false; });

            // Connect 콜백까지 대기 (너무 오래 걸리면 다음 루프로 넘어갈 수 있게 타임아웃을 두는 것도 가능)
            float wait = 0f;
            float connectTimeout = 3.0f;
            while (!connectDone && wait < connectTimeout)
            {
                wait += Time.deltaTime;
                yield return null;
            }

            if (connectOk && GameServer.IsConnected())
            {
                Debug.Log("[Reconnect] Success");
                _isConnectingOrReconnecting = false;

                // 기존 성공 콜백이 있으면 호출(타이틀씬 로직 재사용 가능)
                _onConnectSuccess?.Invoke();
                yield break;
            }

            // 실패 시 backoff 증가
            delay = Mathf.Min(delay * 2f, maxDelay);
        }

        Debug.LogWarning("[Reconnect] Failed after max retries.");
        _isConnectingOrReconnecting = false;

        _onConnectFailed?.Invoke();
    }


    public void Update()
    {
        GameServer.Update();
    }

    public void Send(IMessage packet, EServerType type = EServerType.GameServer)
    {
        if (type == EServerType.GameServer)
            GameServer.Send(packet);
	}
}