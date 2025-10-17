using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine;

public class UI_RoomPopup : UI_Popup
{
    enum GameObjects
    {
        MyPlayerState,
        EnemyPlayerState
    }

    enum Buttons
    {
        CloseRoomButton,
        StartButton,
    }

    enum Texts
    {
        RoomNameLabelText,
        MyPlayerLabelText,
        EnemyPlayerLabelText
    }

    Action _onClosePopup;

    protected override void Awake()
    {
        base.Awake();

        BindObjects(typeof(GameObjects));
        BindButtons(typeof(Buttons));
        BindTexts(typeof(Texts));

        GetButton((int)Buttons.CloseRoomButton).gameObject.BindEvent(OnClickCloseButton);
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);

        GetObject((int)GameObjects.MyPlayerState).gameObject.SetActive(false);
        GetObject((int)GameObjects.EnemyPlayerState).gameObject.SetActive(false);
    }

    public void OnCreateRoomResHandler(S_CreateRoomRes createRoomRes)
    {
        Managers.GameRoom.MyPlayerInfo = Managers.Player.MyPlayerInfo;
        GetText((int)Texts.MyPlayerLabelText).text = Managers.Player.MyPlayerInfo.Name;

        Managers.GameRoom.RoomInfo = createRoomRes.RoomInfo;

        GetText((int)Texts.RoomNameLabelText).text = createRoomRes.RoomInfo.Name;
    }

    public void OnEnterGameHandler(S_EnterGame enterGame)
    {
        Managers.GameRoom.MyPlayerInfo = Managers.Player.MyPlayerInfo;
        GetText((int)Texts.MyPlayerLabelText).text = Managers.GameRoom.MyPlayerInfo.Name;

        List<PlayerInfo> playerInfos = enterGame.PlayerInfos.ToList();

        Managers.Player.EnemyPlayerInfo = playerInfos[0];
        Managers.GameRoom.EnemyPlayerInfo = Managers.Player.EnemyPlayerInfo;
        GetText((int)Texts.EnemyPlayerLabelText).text = Managers.GameRoom.EnemyPlayerInfo.Name;

        Managers.GameRoom.RoomInfo = enterGame.RoomInfo;

        GetText((int)Texts.RoomNameLabelText).text = enterGame.RoomInfo.Name;
    }

    public void OnJoinGameHandler(S_JoinGame joinGame)
    {
        Managers.Player.EnemyPlayerInfo = joinGame.PlayerInfo;
        Managers.GameRoom.EnemyPlayerInfo = Managers.Player.EnemyPlayerInfo;
        GetText((int)Texts.EnemyPlayerLabelText).text = Managers.GameRoom.EnemyPlayerInfo.Name;
    }

    public void OnPlayerStateHandler(S_PlayerState playerState)
    {
        Managers.Player.EnemyPlayerInfo = playerState.PlayerInfo;

        if (playerState.PlayerInfo.State == EPlayerState.Ready)
            GetObject((int)GameObjects.EnemyPlayerState).gameObject.SetActive(true);
        else
            GetObject((int)GameObjects.EnemyPlayerState).gameObject.SetActive(false);
    }

    public void OnStartGameHandler(S_StartGame startGame)
    {
        Managers.GameRoom.RoomInfo = startGame.RoomInfo;

        Managers.Player.MyPlayerInfo.State = EPlayerState.Playing;
        Managers.Player.EnemyPlayerInfo.State = EPlayerState.Playing;

        Managers.Scene.LoadScene(Define.EScene.MultiGameScene);
    }

    public void SetInfo(Action action)
    {
        _onClosePopup = action;
    }

    void OnClickCloseButton(PointerEventData evt)
    {
        ClosePopupUI();
    }

    public void OnClickStartButton(PointerEventData evt)
    {
        if (Managers.Player.MyPlayerInfo.State == EPlayerState.NotReady)
        {
            Managers.Player.MyPlayerInfo.State = EPlayerState.Ready;
            GetObject((int)GameObjects.MyPlayerState).gameObject.SetActive(true);
        }
        else
        {
            Managers.Player.MyPlayerInfo.State = EPlayerState.NotReady;
            GetObject((int)GameObjects.MyPlayerState).gameObject.SetActive(false);
        }

        C_PlayerState playerState = new C_PlayerState();
        playerState.PlayerInfo = Managers.Player.MyPlayerInfo;
        Managers.Network.Send(playerState);
    }

}
