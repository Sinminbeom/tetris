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
    public IPlayer CreatePlayer()
    {
        IPlayer player = new MyPlayer();

        IBoard board = this.multiPlayerInfoFactory.CreateBoard();
        player.Board = board;

        return player;
    }

}
