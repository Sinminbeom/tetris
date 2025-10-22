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
        Managers.GameRoom.MyPlayerInfo = Managers.Player.MyPlayerInfo;
        Managers.GameRoom.RoomInfo = createRoomRes.RoomInfo;

        RefreshUI();
    }

    public void OnEnterGameHandler(S_EnterGame enterGame)
    {
        Managers.GameRoom.RoomInfo = enterGame.RoomInfo;

        Managers.GameRoom.MyPlayerInfo = Managers.Player.MyPlayerInfo;

        List<PlayerInfo> playerInfos = enterGame.PlayerInfos.ToList();

        if (playerInfos.Count > 0)
        {
            Managers.Player.EnemyPlayerInfo = playerInfos[0];
            Managers.GameRoom.EnemyPlayerInfo = Managers.Player.EnemyPlayerInfo;
        }

        RefreshUI();
    }

    public void OnJoinGameHandler(S_JoinGame joinGame)
    {
        Managers.Player.EnemyPlayerInfo = joinGame.PlayerInfo;
        Managers.GameRoom.EnemyPlayerInfo = Managers.Player.EnemyPlayerInfo;

        RefreshUI();
    }

    public void OnLeaveGameHandler(S_LeaveGame leaveGame)
    {
        Managers.GameRoom.SelectedRoomIndex = 0;
        Managers.GameRoom.RoomInfo = null;

        Managers.GameRoom.EnemyPlayer = null;
        Managers.GameRoom.EnemyPlayerInfo = null;

        Managers.GameRoom.MyPlayer = null;
        Managers.GameRoom.MyPlayerInfo = null;

        _onClosePopup?.Invoke();
        ClosePopupUI();
    }

    public void OnLeavePlayerHandler(S_LeavePlayer leavePlayer)
    {
        Managers.Player.EnemyPlayerInfo = null;
        Managers.GameRoom.EnemyPlayerInfo = null;
        Managers.GameRoom.EnemyPlayer = null;

        RefreshUI();
    }

    public void OnPlayerStateHandler(S_PlayerState playerState)
    {
        Managers.Player.EnemyPlayerInfo = playerState.PlayerInfo;
        Managers.GameRoom.EnemyPlayerInfo = Managers.Player.EnemyPlayerInfo;

        RefreshUI();
    }

    public void OnStartGameHandler(S_StartGame startGame)
    {
        Managers.GameRoom.RoomInfo = startGame.RoomInfo;

        Managers.Player.MyPlayerInfo.State = EPlayerState.Playing;
        Managers.Player.EnemyPlayerInfo.State = EPlayerState.Playing;

        Managers.GameRoom.MyPlayerInfo = Managers.Player.MyPlayerInfo;
        Managers.GameRoom.EnemyPlayerInfo = Managers.Player.EnemyPlayerInfo;

        Managers.Scene.LoadScene(Define.EScene.MultiGameScene);
    }

    public void SetInfo(Action action)
    {
        _onClosePopup = action;
    }

    void OnClickCloseButton(PointerEventData evt)
    {
        C_LeaveGame leaveGame = new C_LeaveGame();
        leaveGame.RoomIndex = Managers.GameRoom.SelectedRoomIndex;
        Managers.Network.Send(leaveGame);
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
        GetText((int)Texts.RoomNameLabelText).text = Managers.GameRoom.RoomInfo?.Name ?? "";

        // 내 플레이어 이름
        var myInfo = Managers.GameRoom.MyPlayerInfo;
        GetText((int)Texts.MyPlayerLabelText).text = myInfo?.Name ?? "";

        // 상대 플레이어 이름
        var enemyInfo = Managers.GameRoom.EnemyPlayerInfo;
        GetText((int)Texts.EnemyPlayerLabelText).text = enemyInfo?.Name ?? "";

        // 내 Ready 표시
        bool myReady = myInfo?.State == EPlayerState.Ready;
        GetObject((int)GameObjects.MyPlayerState).gameObject.SetActive(myReady);

        // 상대 Ready 표시
        bool enemyReady = enemyInfo?.State == EPlayerState.Ready;
        GetObject((int)GameObjects.EnemyPlayerState).gameObject.SetActive(enemyReady);
    }
}
