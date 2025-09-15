using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerInfoFactory : IMultiPlayerInfoFactory
{

    public IBoard CreateBoard()
    {
        IBoardFactory boardFactory = new MyBoardFactory();
        IBoard board = boardFactory.CreateBoard();
        return board;
    }
}
