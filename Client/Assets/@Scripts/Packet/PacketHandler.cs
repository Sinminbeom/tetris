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


    }
}

