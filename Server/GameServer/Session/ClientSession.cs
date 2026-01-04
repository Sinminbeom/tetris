using ServerCore;
using System.Net;
using Google.Protobuf;
using GameServer;
	using Google.Protobuf.Protocol;

namespace Server
{
	public partial class ClientSession : PacketSession
	{
		public long AccountDbId { get; set; }
		public int SessionId { get; set; }

		// 마지막으로 패킷을 수신한 시각(서버 틱). 유휴 세션 정리/끊김 보정에 사용.
		public long LastRecvTick { get; private set; } = Environment.TickCount64;
		public void TouchRecv() => LastRecvTick = Environment.TickCount64;

		public Player Player { get; set; }
        object _lock = new object();

		#region Network
		// 예약만 하고 보내지는 않는다
		public void Send(IMessage packet)
		{
			Send(new ArraySegment<byte>(MakeSendBuffer(packet)));
		}

		public static byte[] MakeSendBuffer(IMessage packet)
		{
			MsgId msgId = (MsgId)Enum.Parse(typeof(MsgId), packet.Descriptor.Name);
			ushort size = (ushort)packet.CalculateSize();
			byte[] sendBuffer = new byte[size + 4];
			Array.Copy(BitConverter.GetBytes((ushort)(size + 4)), 0, sendBuffer, 0, sizeof(ushort));
			Array.Copy(BitConverter.GetBytes((ushort)msgId), 0, sendBuffer, 2, sizeof(ushort));
			Array.Copy(packet.ToByteArray(), 0, sendBuffer, 4, size);
			return sendBuffer;
		}

		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnConnected : {endPoint}");
		}

		public override void OnRecvPacket(ArraySegment<byte> buffer)
		{
			PacketManager.Instance.OnRecvPacket(this, buffer);
		}

		public override void OnDisconnected(EndPoint endPoint)
		{
			// 중요: 강제 종료/네트워크 단절 시에도 클라이언트가 LeaveRoom 패킷을 못 보낼 수 있으므로
			//       서버가 룸 멤버십을 authoritative하게 정리해야 한다.
			GameLogic.Instance.Push(CleanupOnDisconnect);

			SessionManager.Instance.Remove(this);
			Console.WriteLine($"OnDisconnected : {endPoint}");
		}

		void CleanupOnDisconnect()
		{
			if (Player == null)
				return;

			GameRoom room = Player.Room;
			if (room == null)
				return;

			// 상대방을 먼저 확보 (LeaveGame 이후에는 못 찾을 수 있음)
			Player otherPlayer = room.GetOtherPlayer(Player);
			room.LeaveGame(Player);

			// 나(끊긴 세션)는 이미 연결이 종료되어 응답 전송 불가
			// 상대방에게는 "상대가 나갔다"를 브로드캐스트
			if (otherPlayer == null)
			{
				GameLogic.Instance.Remove(room.RoomInfo.RoomId);
				return;
			}

			// 룸 상태 복구(대기) 및 상대 플레이어 상태 초기화(필요 시)
			room.RoomInfo.Status = ERoomState.Waiting;
			otherPlayer.PlayerInfo.State = EPlayerState.NotReady;

			S_LeavePlayer leavePlayer = new S_LeavePlayer();
			otherPlayer.Session?.Send(leavePlayer);
		}

		public override void OnSend(int numOfBytes)
		{
			//Console.WriteLine($"Transferred bytes: {numOfBytes}");
		}
		#endregion
	}
}
