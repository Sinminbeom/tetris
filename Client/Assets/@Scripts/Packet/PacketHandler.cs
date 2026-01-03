using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.Linq;

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

        if (signUpPopup == null)
            return;

        S_SignUpRes signUpRes = (S_SignUpRes)packet;
        signUpPopup.OnSignUpHandler(signUpRes);
    }

    public static void S_LogInResHandler(PacketSession session, IMessage packet)
    {
        UI_LogInPopup logInPopup = Managers.UI.GetLastPopupUI<UI_LogInPopup>();

        if (logInPopup == null)
            return;

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

            if (createRoomPopup == null)
                return;

            createRoomPopup.OnCreateRoomResHandler(createRoomRes);
        }
        else
        {

        }
    }

    public static void S_RoomListResHandler(PacketSession session, IMessage packet)
    {
        UI_LobbyScene lobbyScene = Managers.UI.GetSceneUI<UI_LobbyScene>();

        if (lobbyScene == null)
            return;

        S_RoomListRes roomListRes = (S_RoomListRes)packet;
        lobbyScene.OnRoomListResHandler(roomListRes);
    }

    public static void S_EnterRoomHandler(PacketSession session, IMessage packet)
    {
        UI_RoomPopup roomPopup = Managers.UI.GetLastPopupUI<UI_RoomPopup>();

        if (roomPopup == null)
            return;

        S_EnterRoom enterRoom = (S_EnterRoom)packet;
        roomPopup.OnEnterRoomHandler(enterRoom);

    }

    public static void S_JoinRoomHandler(PacketSession session, IMessage packet)
    {
        UI_RoomPopup roomPopup = Managers.UI.GetLastPopupUI<UI_RoomPopup>();

        S_JoinRoom joinRoom = (S_JoinRoom)packet;

        if (roomPopup == null)
        {
            Managers.Room.JoinRoom(joinRoom);
        }
        else
        {
            roomPopup.OnJoinRoomHandler(joinRoom);
        }
    }

    public static void S_LeaveRoomHandler(PacketSession session, IMessage packet)
    {
        UI_RoomPopup roomPopup = Managers.UI.GetLastPopupUI<UI_RoomPopup>();

        if (roomPopup == null)
            return;

        S_LeaveRoom leaveRoom = (S_LeaveRoom)packet;
        roomPopup.OnLeaveRoomHandler(leaveRoom);
    }

    public static void S_LeavePlayerHandler(PacketSession session, IMessage packet)
    {
        UI_RoomPopup roomPopup = Managers.UI.GetLastPopupUI<UI_RoomPopup>();

        S_LeavePlayer leavePlayer = (S_LeavePlayer)packet;

        if (roomPopup == null)
        {
            Managers.Room.LeavePlayer(leavePlayer);
        }
        else
        {
            roomPopup.OnLeavePlayerHandler(leavePlayer);
        }
    }

    public static void S_PlayerStateHandler(PacketSession session, IMessage packet)
    {
        UI_RoomPopup roomPopup = Managers.UI.GetLastPopupUI<UI_RoomPopup>();

        S_PlayerState playerState = (S_PlayerState)packet;

        if (roomPopup == null)
        {
            Managers.Room.PlayerState(playerState);
        }
        else
        {
            roomPopup.OnPlayerStateHandler(playerState);
        }


    }

    public static void S_StartGameHandler(PacketSession session, IMessage packet)
    {
        UI_RoomPopup roomPopup = Managers.UI.GetLastPopupUI<UI_RoomPopup>();

        if (roomPopup == null)
            return;

        S_StartGame startGame = (S_StartGame)packet;
        roomPopup.OnStartGameHandler(startGame);
    }

    public static void S_SpawnTetrominoHandler(PacketSession session, IMessage packet)
    {
        S_SpawnTetromino spawnTetromino = (S_SpawnTetromino)packet;
        Managers.Room.EnemyPlayer.Board.Spawn(spawnTetromino.TetrominoType);
    }

    public static void S_MoveTetrominoHandler(PacketSession session, IMessage packet)
    {
        S_MoveTetromino moveTetromino = (S_MoveTetromino)packet;

        int x = moveTetromino.PositionInfo.PosX - Managers.Room.MyPlayer.Board.Pos.x + Managers.Room.EnemyPlayer.Board.Pos.x;
        int y = moveTetromino.PositionInfo.PosY - Managers.Room.MyPlayer.Board.Pos.y + Managers.Room.EnemyPlayer.Board.Pos.y;
        Vector3 vector3 = new Vector3(x, y, 0);

        Managers.Room.EnemyPlayer.Board.SyncMove(vector3, moveTetromino.PositionInfo.IsRotation);
    }

    public static void S_LockBlockHandler(PacketSession session, IMessage packet)
    {
        S_LockBlock lockBlock = (S_LockBlock)packet;

        Managers.Room.EnemyPlayer.Board.SyncAddObject();
    }

    public static void S_ClearRowsHandler(PacketSession session, IMessage packet)
    {
        S_ClearRows clearRows = (S_ClearRows)packet;

        Managers.Room.EnemyPlayer.Board.ClearRows(clearRows.Rows.ToList());
    }

    public static void S_GameOverHandler(PacketSession session, IMessage packet)
    {
        S_GameOver gameOver = (S_GameOver)packet;

        UI_GameOverPopup gameOverPopup = Managers.UI.ShowPopupUI<UI_GameOverPopup>();
        gameOverPopup.OnGameOver(true);

        MyTetromino myTetromino = (MyTetromino)Managers.Room.MyPlayer.Board.Tetromino;
        myTetromino.ChangeState((int)E_TETROMINO_STATE.Idle);
    }

    public static void S_PongHandler(PacketSession session, IMessage packet)
    {

    }
}

