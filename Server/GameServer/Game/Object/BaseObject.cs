using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
	public class BaseObject
	{
		public EGameObjectType ObjectType { get; protected set; } = EGameObjectType.None;
		public int ObjectId
		{
			get { return ObjectInfo.ObjectId; }
			set { ObjectInfo.ObjectId = value; }
		}

		// player에 room을 넣어야할듯
		//public GameRoom Room { get; set; }

		public ObjectInfo ObjectInfo { get; set; } = new ObjectInfo();
		public PositionInfo PosInfo { get; private set; } = new PositionInfo();

		public EObjectState State
		{
			get { return PosInfo.State; }
			set { PosInfo.State = value; }
		}

		public BaseObject()
		{
			ObjectInfo.PosInfo = PosInfo;
		}

		public virtual void Update()
		{

		}

		public Vector2Int Pos
		{
			get
			{
				return new Vector2Int(PosInfo.PosX, PosInfo.PosY);
			}

			set
			{
				PosInfo.PosX = value.x;
				PosInfo.PosY = value.y;
			}
		}

		public void BroadcastMove()
		{
			// 다른 플레이어한테도 알려준다
			//S_Move movePacket = new S_Move();
			//movePacket.ObjectId = ObjectId;
			//movePacket.PosInfo = PosInfo;
			//Room?.Broadcast(Pos, movePacket);
		}
	}
}
