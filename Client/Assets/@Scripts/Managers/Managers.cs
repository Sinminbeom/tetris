using UnityEngine;
using UnityEngine.SceneManagement;
using Google.Protobuf.Protocol;

public class Managers : MonoBehaviour
{
    public static bool Initialized { get; set; }
    private static Managers s_instance; // 유일성이 보장된다
    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    #region Contents

    private RoomManager _room = new RoomManager();
    private ObjectManager _object = new ObjectManager();
    private PlayerManager _player = new PlayerManager();

    public static RoomManager Room { get { return Instance?._room; } }
    public static ObjectManager Object { get { return Instance?._object; } }
    public static PlayerManager Player { get { return Instance?._player; } }

    #endregion

    #region Core

    private DataManager _data = new DataManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private UIManager _ui = new UIManager();
    private NetworkManager _network = new NetworkManager();

    public static DataManager Data { get { return Instance?._data; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static SceneManagerEx Scene { get { return Instance?._scene; } }
    public static UIManager UI { get { return Instance?._ui; } }
    public static NetworkManager Network { get { return Instance?._network; } }

    #endregion

    public static void Init()
    {
        if (s_instance == null && Initialized == false)
        {
            Initialized = true;

            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._network.SetRunner(s_instance);
        }
    }
    // Update is called once per frame
    public void Update()
    {
        _network?.Update();
    }


	void OnApplicationPause(bool pause)
	{
        if (pause)
        {
            BestEffortLeaveRoom();
            return;
        }

        // 포그라운드 복귀
        _network?.TryAutoReconnect();
    }

	void OnApplicationQuit()
	{
		BestEffortLeaveRoom();
	}

	void BestEffortLeaveRoom()
	{
		// 강제 종료/백그라운드 전환에서는 전송이 보장되지 않습니다.
		// 다만 가능한 경우 서버가 즉시 룸 정리를 할 수 있도록 LeaveRoom을 best-effort로 송신합니다.
		if (_network == null)
			return;
		if (!_network.GameServer.IsConnected())
			return;
		if (Room == null || Room.RoomInfo == null)
			return;

		var leave = new C_LeaveRoom { RoomIndex = Room.RoomInfo.RoomId };
		_network.Send(leave);
	}

    public static void Clear()
    {
        UI.Clear();
    }
}