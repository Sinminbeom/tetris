using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RoomPopup : UI_Popup
{

    enum Buttons
    {
        CloseRoomButton,
        StartButton,
    }

    enum Texts
    {
        MyPlayerLabelText,
        EnemyPlayerLabelText
    }

    Action _onClosePopup;

    protected override void Awake()
    {
        base.Awake();

        BindButtons(typeof(Buttons));
        BindTexts(typeof(Texts));

        GetButton((int)Buttons.CloseRoomButton).gameObject.BindEvent(OnClickCloseButton);
    }

    public void OnCreateRoomResHandler(S_CreateRoomRes createRoomRes)
    {
        Managers.GameRoom.SetPlayer(Managers.Player.PlayerInfo, GameRoomManager.PlayerType.MyPlayer);
        GetText((int)Texts.MyPlayerLabelText).text = Managers.Player.PlayerInfo.Name;
    }

    public void OnEnterGameHandler(S_EnterGame enterGame)
    {
        Managers.GameRoom.SetPlayer(Managers.Player.PlayerInfo, GameRoomManager.PlayerType.MyPlayer);
        GetText((int)Texts.MyPlayerLabelText).text = Managers.Player.PlayerInfo.Name;

        List<PlayerInfo> playerInfos = enterGame.PlayerInfos.ToList();

        Managers.GameRoom.SetPlayer(playerInfos[0], GameRoomManager.PlayerType.EnemyPlayer);
        GetText((int)Texts.EnemyPlayerLabelText).text = playerInfos[0].Name;
    }

    public void OnJoinGameHandler(S_JoinGame joinGame)
    {
        Managers.GameRoom.SetPlayer(joinGame.PlayerInfo, GameRoomManager.PlayerType.EnemyPlayer);
        GetText((int)Texts.EnemyPlayerLabelText).text = joinGame.PlayerInfo.Name;
    }

    public void SetInfo(Action action)
    {
        _onClosePopup = action;
    }

    void OnClickCloseButton(PointerEventData evt)
    {
        ClosePopupUI();
    }

}
