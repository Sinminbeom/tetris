using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CreateRoomPopup : UI_Popup
{

    enum Buttons
    {
        CreateRoomButton,
        CloseButton,
    }

    enum Texts
    {
        RoomNameText,
        CreateLabelText,
    }
    
    protected override void Awake()
    {
        base.Awake();

        BindButtons(typeof(Buttons));
        BindTexts(typeof(Texts));

        GetButton((int)Buttons.CreateRoomButton).gameObject.BindEvent(OnClickCreatePlayerButton);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnClickCloseButton);
    }

    public void SetInfo(Action<int> action)
    {

    }

    void OnClickCreatePlayerButton(PointerEventData evt)
    {
        Debug.Log("OnClickCreateCharacterButton");

        // 1) 서버로 C_CreateHeroReq 패킷 전송
        // 2) 서버에서 DB로 이름 체크 후 생성
        // 3) 서버에서 S_CreateHeroRes 패킷 답신
        //C_CreateHeroReq reqPacket = new C_CreateHeroReq();

        //reqPacket.ClassType = GetClassType();
        //reqPacket.Gender = GetGender();
        //reqPacket.Name = GetName();

        //Managers.Network.Send(reqPacket);
    }

    void OnClickCloseButton(PointerEventData evt)
    {
        ClosePopupUI();
    }
}
