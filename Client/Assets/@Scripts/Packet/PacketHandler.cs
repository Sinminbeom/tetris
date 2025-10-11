using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.Linq;
using static Define;

class PacketHandler
{
    ///////////////////////////////////// GameServer - Client /////////////////////////////////////
    public static void S_ConnectedHandler(PacketSession session, IMessage packet)
    {
        Debug.Log("S_Connected");
    }

    public static void S_SignUpResHandler(PacketSession session, IMessage packet)
    {
        UI_SignUpPopup signUpPopup = Managers.UI.GetLastPopupUI<UI_SignUpPopup>();
        S_SignUpRes signUpRes = (S_SignUpRes)packet;
        signUpPopup.OnSignUpHandler(signUpRes);
    }

    public static void S_LogInResHandler(PacketSession session, IMessage packet)
    {
        UI_LogInPopup logInPopup = Managers.UI.GetLastPopupUI<UI_LogInPopup>();
        S_LogInRes logInRes = (S_LogInRes)packet;
        logInPopup.OnLogInResHandler(logInRes);
    }

    public static void S_DeleteRoomResHandler(PacketSession session, IMessage packet)
    {
        //UI_SelectRoomPopup popupUI = Managers.UI.GetLastPopupUI<UI_SelectRoomPopup>();
        //if (popupUI == null)
        //    return;

        //S_DeleteRoomRes resPacket = packet as S_DeleteRoomRes;
        //popupUI.OnDeleteRoomResHandler(resPacket);
    }

    public static void S_CreateRoomResHandler(PacketSession session, IMessage packet)
    {
        S_CreateRoomRes createRoomRes = (S_CreateRoomRes)packet;
        if (createRoomRes.Result == ECreateRoomResult.Success)
        {
            UI_CreateRoomPopup createRoomPopup = Managers.UI.GetLastPopupUI<UI_CreateRoomPopup>();
            createRoomPopup.OnCreateRoomResHandler(createRoomRes);
        }
        else
        {

        }
    }

    public static void S_RoomListResHandler(PacketSession session, IMessage packet)
    {
        UI_LobbyScene lobbyScene = Managers.UI.GetSceneUI<UI_LobbyScene>();
        S_RoomListRes roomListRes = (S_RoomListRes)packet;
        lobbyScene.OnRoomListResHandler(roomListRes);
    }

    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        UI_RoomPopup roomPopup = Managers.UI.GetLastPopupUI<UI_RoomPopup>();
        S_EnterGame enterGame = (S_EnterGame)packet;
        roomPopup.OnEnterGameHandler(enterGame);

    }

    public static void S_JoinGameHandler(PacketSession session, IMessage packet)
    {
        UI_RoomPopup roomPopup = Managers.UI.GetLastPopupUI<UI_RoomPopup>();
        S_JoinGame joinGame = (S_JoinGame)packet;
        roomPopup.OnJoinGameHandler(joinGame);
    }

    public static void S_PlayerStateHandler(PacketSession session, IMessage packet)
    {
        UI_RoomPopup roomPopup = Managers.UI.GetLastPopupUI<UI_RoomPopup>();
        S_PlayerState playerState = (S_PlayerState)packet;
        roomPopup.OnPlayerStateHandler(playerState);
    }

    public static void S_StartGameHandler(PacketSession session, IMessage packet)
    {
        UI_RoomPopup roomPopup = Managers.UI.GetLastPopupUI<UI_RoomPopup>();
        S_StartGame startGame = (S_StartGame)packet;
        roomPopup.OnStartGameHandler(startGame);
    }

    public static void S_SpawnTetrominoHandler(PacketSession session, IMessage packet)
    {
        S_SpawnTetromino spawnTetromino = (S_SpawnTetromino)packet;
        Managers.GameRoom.EnemyPlayer.Board.Spawn(spawnTetromino.TetrominoType);
    }

    public static void S_MoveTetrominoHandler(PacketSession session, IMessage packet)
    {
        S_MoveTetromino moveTetromino = (S_MoveTetromino)packet;

        int x = moveTetromino.PositionInfo.PosX - Managers.GameRoom.MyPlayer.Board.Pos.x + Managers.GameRoom.EnemyPlayer.Board.Pos.x;
        int y = moveTetromino.PositionInfo.PosY - Managers.GameRoom.MyPlayer.Board.Pos.y + Managers.GameRoom.EnemyPlayer.Board.Pos.y;
        Vector3 vector3 = new Vector3(x, y, 0);

        Managers.GameRoom.EnemyPlayer.Board.SyncMove(vector3, moveTetromino.PositionInfo.IsRotation);
    }

    public static void S_LockBlockHandler(PacketSession session, IMessage packet)
    {
        S_LockBlock lockBlock = (S_LockBlock)packet;

        Managers.GameRoom.EnemyPlayer.Board.SyncAddObject();
    }
}

