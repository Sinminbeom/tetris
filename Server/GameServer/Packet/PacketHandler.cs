using Google.Protobuf;
using Server;
using GameServer;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Data;
using Google.Protobuf.Protocol;
using System.Numerics;

class PacketHandler
{
    ///////////////////////////////////// Client - Game Server /////////////////////////////////////
   
	public static void C_EnterGameHandler(PacketSession session, IMessage packet)
	{
		//C_EnterGame enterGamePacket = (C_EnterGame)packet;
		//ClientSession clientSession = (ClientSession)session;
		//clientSession.HandleEnterGame(enterGamePacket);
	}

	public static void C_MoveHandler(PacketSession session, IMessage packet)
	{
		//C_Move movePacket = (C_Move)packet;
		//ClientSession clientSession = (ClientSession)session;

		//Tetromino tetromino = clientSession.MyTetromino;
		//if (tetromino == null)
		//	return;

		//GameRoom room = tetromino.Room;
		//if (room == null)
		//	return;

		//room.Push(room.HandleMove, tetromino, movePacket);
	}

}
