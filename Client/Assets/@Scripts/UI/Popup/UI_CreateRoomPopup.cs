using Google.Protobuf.Protocol;
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
        CreateRoomButton
    }

    enum Texts
    {
        RoomNameText,
        CreateLabelText,
    }

    //Action OnRoomChanged;

    protected override void Awake()
    {
        base.Awake();

        BindButtons(typeof(Buttons));
        BindTexts(typeof(Texts));

        GetButton((int)Buttons.CreateRoomButton).gameObject.BindEvent(OnClickCreatePlayerButton);
    }

    //public void SetInfo(Action onRoomChanged)
    //{
    //    OnRoomChanged = onRoomChanged;
    //}

    void OnClickCreatePlayerButton(PointerEventData evt)
    {
        Debug.Log("OnClickCreateCharacterButton");

        // 1) 서버로 C_CreateHeroReq 패킷 전송
        // 2) 서버에서 DB로 이름 체크 후 생성
        // 3) 서버에서 S_CreateHeroRes 패킷 답신
        C_CreateRoomReq reqPacket = new C_CreateRoomReq();

        reqPacket.Name = GetName();

        Managers.Network.Send(reqPacket);
    }

    public string GetName()
    {
        return GetText((int)Texts.RoomNameText).text;
    }

    public void OnCreateRoomResHandler(S_CreateRoomRes createRoomRes)
    {
        //OnRoomChanged?.Invoke();
        //ClosePopupUI();
        if (createRoomRes.Result == ECreateRoomResult.Success)
        {
            UI_RoomPopup roomPopup = Managers.UI.ShowPopupUI<UI_RoomPopup>();
            roomPopup.OnCreateRoomResHandler(createRoomRes);
        }
    }
}
