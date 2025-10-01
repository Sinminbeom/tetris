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
}

