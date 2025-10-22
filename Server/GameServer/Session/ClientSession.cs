using ServerCore;
using System.Net;
using Google.Protobuf;
using GameServer;

namespace Server
{
	public partial class ClientSession : PacketSession
	{
		public long AccountDbId { get; set; }
		public int SessionId { get; set; }

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
            GameLogic.Instance.Push(() =>
            {
                if (Player == null)
                    return;

                //GameLogic.Instance.Remove(Player.Room.RoomInfo.RoomId);

                //GameRoom room = GameLogic.Instance.Find(1);
                // TODO
                //room.Push(room.LeaveGame, MyHero.ObjectId, false);
            });

            SessionManager.Instance.Remove(this);

			Console.WriteLine($"OnDisconnected : {endPoint}");
		}

		public override void OnSend(int numOfBytes)
		{
			//Console.WriteLine($"Transferred bytes: {numOfBytes}");
		}
		#endregion
	}
}
