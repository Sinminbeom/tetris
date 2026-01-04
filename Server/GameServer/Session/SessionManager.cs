using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
	class SessionManager : Singleton<SessionManager>
	{
		int _sessionId = 0;

		Dictionary<int, ClientSession> _sessions = new Dictionary<int, ClientSession>();
		object _lock = new object();

		public List<ClientSession> GetSessions()
		{
			List<ClientSession> sessions = new List<ClientSession>();

			lock (_lock)
			{
				sessions = _sessions.Values.ToList();
			}

			return sessions;
		}

		public ClientSession Generate()
		{
			lock (_lock)
			{
				int sessionId = ++_sessionId;

				ClientSession session = new ClientSession();
				session.SessionId = sessionId;
				_sessions.Add(sessionId, session);

				Console.WriteLine($"Connected : {sessionId}");

				return session;
			}
		}

		public ClientSession Find(int id)
		{
			lock (_lock)
			{
				_sessions.TryGetValue(id, out ClientSession session);
				return session;
			}
		}

		public void Remove(ClientSession session)
		{
			lock (_lock)
			{
				_sessions.Remove(session.SessionId);
			}
		}

		/// <summary>
		/// 클라이언트가 강제 종료/네트워크 단절로 LeaveRoom 패킷을 보내지 못하는 경우를 대비해,
		/// 마지막 패킷 수신 시각(LastRecvTick)을 기준으로 유휴 세션을 정리합니다.
		/// </summary>
		public void SweepIdleSessions()
		{
			// GameLogic 쓰레드에서 주기적으로 호출되는 것을 전제(불필요한 락 경쟁 방지)
			long now = Environment.TickCount64;
			int idleMs = GameServer.ConfigManager.Config.idleTimeoutSeconds * 1000;
			int inRoomMs = GameServer.ConfigManager.Config.inRoomTimeoutSeconds * 1000;

			foreach (ClientSession session in GetSessions())
			{
				try
				{
					int threshold = idleMs;
					if (session.Player != null && session.Player.Room != null)
						threshold = inRoomMs;

					long delta = now - session.LastRecvTick;
					if (delta > threshold)
					{
						// Disconnect는 thread-safe하며, OnDisconnected에서 룸 정리 로직이 실행됨
						session.Disconnect();
					}
				}
				catch
				{
					// best-effort: sweep 중 예외가 전체 루프를 깨지 않도록 방어
				}
			}
		}
	}
}
