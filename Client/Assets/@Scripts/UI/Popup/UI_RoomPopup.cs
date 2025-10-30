using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

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
        Managers.Room.CreateRoom(createRoomRes);

        RefreshUI();
    }

    public void OnEnterRoomHandler(S_EnterRoom enterRoom)
    {
        Managers.Room.EnterRoom(enterRoom);

        RefreshUI();
    }

    public void OnJoinRoomHandler(S_JoinRoom joinRoom)
    {
        Managers.Room.JoinRoom(joinRoom);

        RefreshUI();
    }

    public void OnLeaveRoomHandler(S_LeaveRoom leaveGame)
    {
        Managers.Room.LeaveRoom(leaveGame);

        _onClosePopup?.Invoke();
        ClosePopupUI();
    }

    public void OnLeavePlayerHandler(S_LeavePlayer leavePlayer)
    {
        Managers.Room.LeavePlayer(leavePlayer);

        RefreshUI();
    }

    public void OnPlayerStateHandler(S_PlayerState playerState)
    {
        Managers.Room.PlayerState(playerState);

        RefreshUI();
    }

    public void OnStartGameHandler(S_StartGame startGame)
    {
        Managers.Room.RoomInfo = startGame.RoomInfo;

        Managers.Scene.LoadScene(Define.EScene.MultiGameScene);
    }

    public void SetInfo(Action action)
    {
        _onClosePopup = action;
    }

    void OnClickCloseButton(PointerEventData evt)
    {
        C_LeaveRoom leaveRoom = new C_LeaveRoom();
        leaveRoom.RoomIndex = Managers.Room.SelectedRoomIndex;
        Managers.Network.Send(leaveRoom);
    }

    public void OnClickStartButton(PointerEventData evt)
    {
        ToggleReadyState();

        C_PlayerState playerState = new C_PlayerState();
        playerState.PlayerInfo = Managers.Player.MyPlayerInfo;
        Managers.Network.Send(playerState);
    }

    void ToggleReadyState()
    {
        bool isReady = Managers.Player.MyPlayerInfo.State == EPlayerState.Ready;
        Managers.Player.MyPlayerInfo.State = isReady ? EPlayerState.NotReady : EPlayerState.Ready;

        RefreshUI();
    }

    public void RefreshUI()
    {
        // 방 이름
        GetText((int)Texts.RoomNameLabelText).text = Managers.Room.RoomInfo?.Name ?? "";

        // 내 플레이어 이름
        var myInfo = Managers.Room.MyPlayerInfo;
        GetText((int)Texts.MyPlayerLabelText).text = myInfo?.Name ?? "";

        // 상대 플레이어 이름
        var enemyInfo = Managers.Room.EnemyPlayerInfo;
        GetText((int)Texts.EnemyPlayerLabelText).text = enemyInfo?.Name ?? "";

        // 내 Ready 표시
        bool myReady = myInfo?.State == EPlayerState.Ready;
        GetObject((int)GameObjects.MyPlayerState).gameObject.SetActive(myReady);

        // 상대 Ready 표시
        bool enemyReady = enemyInfo?.State == EPlayerState.Ready;
        GetObject((int)GameObjects.EnemyPlayerState).gameObject.SetActive(enemyReady);
    }
}
