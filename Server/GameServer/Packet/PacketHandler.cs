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
		C_EnterGame enterGamePacket = (C_EnterGame)packet;
		ClientSession clientSession = (ClientSession)session;
		clientSession.HandleEnterGame(enterGamePacket);
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
	
	public static void C_SignUpReqHandler(PacketSession session, IMessage packet)
	{
        C_SignUpReq signUpReq = (C_SignUpReq)packet;
        ClientSession clientSession = (ClientSession)session;
        clientSession.HandleSignUpReq(signUpReq);
    }

	public static void C_LogInReqHandler(PacketSession session, IMessage packet)
	{
        C_LogInReq logInReq = (C_LogInReq)packet;
        ClientSession clientSession = (ClientSession)session;
        clientSession.HandleLonInReq(logInReq);
    }

	public static void C_DeleteRoomReqHandler(PacketSession session, IMessage packet)
	{
        C_DeleteRoomReq deleteRoomReq = (C_DeleteRoomReq)packet;
        ClientSession clientSession = (ClientSession)session;
        clientSession.HandleDeleteRoomReq(deleteRoomReq);
    }

    public static void C_CreateRoomReqHandler(PacketSession session, IMessage packet)
	{
        C_CreateRoomReq createRoomReq = (C_CreateRoomReq)packet;
        ClientSession clientSession = (ClientSession)session;
        clientSession.HandleCreateRoomReq(createRoomReq);
    }

	public static void C_RoomListReqHandler(PacketSession session, IMessage packet)
	{
        C_RoomListReq roomListReq = (C_RoomListReq)packet;
        ClientSession clientSession = (ClientSession)session;
		clientSession.HandleRoomListReq(roomListReq);
    }
}
