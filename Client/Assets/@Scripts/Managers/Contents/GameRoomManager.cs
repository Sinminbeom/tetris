using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomManager
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

    public void StartGame()
    {
        Load(MyPlayerInfo, PlayerType.MyPlayer);
        Load(EnemyPlayerInfo, PlayerType.EnemyPlayer);

        MyPlayer.Init();
        EnemyPlayer.Init();

        RoomInfo.Status = ERoomState.InProgress;
    }

    public void GameOver()
    {
        RoomInfo.Status = ERoomState.Waiting;
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
