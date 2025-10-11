using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerFactory : IMultiPlayerFactory
{
    private IMultiPlayerInfoFactory multiPlayerInfoFactory;

    public MyPlayerFactory(IMultiPlayerInfoFactory multiPlayerInfoFactory)
    {
        this.multiPlayerInfoFactory = multiPlayerInfoFactory;
    }
    public IPlayer CreatePlayer(PlayerInfo playerInfo)
    {
        IPlayer player = new MyPlayer();

        IBoard board = this.multiPlayerInfoFactory.CreateBoard();
        player.Board = board;
        player.PlayerInfo = playerInfo;

        return player;
    }

}
