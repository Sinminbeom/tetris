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
		//public void HandleMove(Tetromino tetromino, C_Move movePacket)
		//{
		//	if (tetromino == null)
		//		return;
		//	if (tetromino.State == EObjectState.Dead)
		//		return;
			
		//	PositionInfo movePosInfo = movePacket.PosInfo;
		//	ObjectInfo info = tetromino.ObjectInfo;

		//	// TODO : 거리 검증 등
		//	//if (Map.CanGo(tetromino, new Vector2Int(movePosInfo.PosX, movePosInfo.PosY)) == false)
		//	//	return;

		//	//info.PosInfo.State = movePosInfo.State;
		//	//info.PosInfo.MoveDir = movePosInfo.MoveDir;
		//	//Map.ApplyMove(hero, new Vector2Int(movePosInfo.PosX, movePosInfo.PosY));

		//	tetromino.BroadcastMove();
		//}
	}
}
