using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomManager
{
    public enum PlayerType
    {
        MyPlayer,
        EnemyPlayer
    };
    public int RoomId { get; set; }
    private IPlayer MyPlayer { get; set; }
    private IPlayer EnemyPlayer { get; set; }

    public void Init()
    {
        // 이미 다른화면에서 SetPlayer로 플레이어들을 다 만들어놨을거임
        {
            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.Name = "minbeom";
            playerInfo.PlayerId = 1;
            SetPlayer(playerInfo, PlayerType.MyPlayer);
        }
        {
            PlayerInfo playerInfo1 = new PlayerInfo();
            playerInfo1.Name = "subin";
            playerInfo1.PlayerId = 2;
            SetPlayer(playerInfo1, PlayerType.EnemyPlayer);
        }

        MyPlayer.Init();
        EnemyPlayer.Init();
    }
    public void SetPlayer(PlayerInfo player, PlayerType type)
    {
        switch (type)
        {
            case PlayerType.MyPlayer:
                IPlayerFactory myPlayerFactory = new MyPlayerFactory(new MyPlayerInfoFactory());
                IPlayer _myPlayer = myPlayerFactory.CreatePlayer();
                MyPlayer = _myPlayer;


                break;
            case PlayerType.EnemyPlayer:
                IPlayerFactory enemyPlayerFactory = new EnemyPlayerFactory(new EnemyPlayerInfoFactory());
                IPlayer _enemyPlayer = enemyPlayerFactory.CreatePlayer();
                EnemyPlayer = _enemyPlayer;
                break;
        }
    }

}
