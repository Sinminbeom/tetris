using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class RoomManager
{
    public int SelectedRoomIndex { get; set; }

    public enum PlayerType
    {
        MyPlayer,
        EnemyPlayer
    };

    public RoomInfo RoomInfo { get; set; }
    public IPlayer MyPlayer { get; set; }
    public IPlayer EnemyPlayer { get; set; }

    public PlayerInfo MyPlayerInfo { get; set; }
    public PlayerInfo EnemyPlayerInfo { get; set; }

    public void CreateRoom(S_CreateRoomRes createRoomRes)
    {
        MyPlayerInfo = Managers.Player.MyPlayerInfo;
        RoomInfo = createRoomRes.RoomInfo;
    }

    public void EnterRoom(S_EnterRoom enterRoom)
    {
        RoomInfo = enterRoom.RoomInfo;

        MyPlayerInfo = Managers.Player.MyPlayerInfo;

        List<PlayerInfo> playerInfos = enterRoom.PlayerInfos.ToList();

        if (playerInfos.Count > 0)
        {
            Managers.Room.EnemyPlayerInfo = playerInfos[0];
        }
    }

    public void LeaveRoom(S_LeaveRoom leaveRoom)
    {
        Managers.Room.SelectedRoomIndex = 0;

        RoomInfo = null;

        EnemyPlayer = null;
        EnemyPlayerInfo = null;

        MyPlayer = null;
        MyPlayerInfo = null;
    }

    public void LeavePlayer(S_LeavePlayer leavePlayer)
    {
        EnemyPlayerInfo = null;
        EnemyPlayer = null;
    }

    public void PlayerState(S_PlayerState playerState)
    {
        EnemyPlayerInfo = playerState.PlayerInfo;
    }

    public void JoinRoom(S_JoinRoom joinRoom)
    {
        EnemyPlayerInfo = joinRoom.PlayerInfo;
    }

    public void StartGame()
    {
        RoomInfo.Status = ERoomState.InProgress;
        MyPlayerInfo.State = EPlayerState.Playing;
        EnemyPlayerInfo.State = EPlayerState.Playing;

        Load(MyPlayerInfo, PlayerType.MyPlayer);
        Load(EnemyPlayerInfo, PlayerType.EnemyPlayer);

        MyPlayer.Init();
        EnemyPlayer.Init();
    }

    public void GameOver()
    {
        RoomInfo.Status = ERoomState.Waiting;

        MyPlayerInfo.State = EPlayerState.NotReady;
        EnemyPlayerInfo.State = EPlayerState.NotReady;
    }

    public void Load(PlayerInfo playerInfo, PlayerType type)
    {
        switch (type)
        {
            case PlayerType.MyPlayer:
                IPlayerFactory myPlayerFactory = new MyPlayerFactory(new MyPlayerInfoFactory());
                IPlayer _myPlayer = myPlayerFactory.CreatePlayer(playerInfo);
                MyPlayer = _myPlayer;


                break;
            case PlayerType.EnemyPlayer:
                IPlayerFactory enemyPlayerFactory = new EnemyPlayerFactory(new EnemyPlayerInfoFactory());
                IPlayer _enemyPlayer = enemyPlayerFactory.CreatePlayer(playerInfo);
                EnemyPlayer = _enemyPlayer;
                break;
        }
    }

}
