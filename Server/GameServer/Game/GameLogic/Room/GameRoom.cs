using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
	public partial class GameRoom : JobSerializer
	{
		public int GameRoomId { get; set; }
		public int TemplateId { get; set; }

		Dictionary<int, Player> _players = new Dictionary<int, Player>();

		public void Init(int mapTemplateId, int zoneCells)
		{
			TemplateId = mapTemplateId;
		}

		// 누군가 주기적으로 호출해줘야 한다
		public void Update()
		{
			//Console.WriteLine($"TimerCount : {TimerCount}");
			//Console.WriteLine($"JobCount : {JobCount}");
			Flush();
		}

		/*
		public void EnterGame(BaseObject obj, bool respawn = false, Vector2Int? pos = null)
		{
			if (obj == null)
				return;

			if (pos.HasValue)
				obj.Pos = pos.Value;
			else
				obj.Pos = GetRandomSpawnPos(obj, checkObjects: true);

			EGameObjectType type = ObjectManager.GetObjectTypeFromId(obj.ObjectId);
			if (type == EGameObjectType.Hero)
			{
				Tetromino hero = (Tetromino)obj;
				_heroes.Add(obj.ObjectId, hero);
				hero.Room = this;

				Map.ApplyMove(hero, new Vector2Int(hero.Pos.x, hero.Pos.y));
				GetZone(hero.Pos).Heroes.Add(hero);

				hero.State = EObjectState.Idle;
				hero.Update();

				// 입장한 사람한테 패킷 보내기.
				{
					S_EnterGame enterPacket = new S_EnterGame();
					enterPacket.MyHeroInfo = hero.MyHeroInfo;
					enterPacket.Respawn = respawn;

					hero.Session?.Send(enterPacket);

					hero.Vision?.Update();
				}

				// 다른 사람들한테 입장 알려주기.
				S_Spawn spawnPacket = new S_Spawn();
				spawnPacket.Heroes.Add(hero.HeroInfo);
				Broadcast(obj.Pos, spawnPacket);
			}
		}

		public void LeaveGame(int objectId, bool kick = false)
		{
			EGameObjectType type = ObjectManager.GetObjectTypeFromId(objectId);

			Vector2Int Pos;

			if (type == EGameObjectType.Hero)
			{
				if (_heroes.Remove(objectId, out Tetromino tetromino) == false)
					return;

				//OnLeaveGame(hero);

				Pos = hero.Pos;

				Map.ApplyLeave(hero);
				hero.Room = null;

				// 본인한테 정보 전송
				{
					S_LeaveGame leavePacket = new S_LeaveGame();
					hero.Session?.Send(leavePacket);
				}

				if (kick)
				{
					// 로비로 강퇴
					//S_Kick kickPacket = new S_Kick();
					//player.Session?.Send(kickPacket);
				}
			}
			else
			{
				return;
			}

			// 타인한테 정보 전송
			{
				S_Despawn despawnPacket = new S_Despawn();
				despawnPacket.ObjectIds.Add(objectId);
				Broadcast(Pos, despawnPacket);
			}
		}
		*/
		public void Broadcast(Vector2Int pos, IMessage packet)
		{;
			byte[] packetBuffer = ClientSession.MakeSendBuffer(packet);

			// 패킷 보내기
			//foreach (Hero p in zones.SelectMany(z => z.Heroes))
			//{
			//	int dx = p.Pos.x - pos.x;
			//	int dy = p.Pos.y - pos.y;
			//	if (Math.Abs(dx) > GameRoom.VisionCells)
			//		continue;
			//	if (Math.Abs(dy) > GameRoom.VisionCells)
			//		continue;

			//	p.Session?.Send(packetBuffer);
			//}
		}
	}
}
