using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SingleBoardFactory : IBoardFactory
{

    public IBoard CreateBoard()
    {
        IBoard board = new SingleBoard();

        IBackground background = new SingleBackground();
        background.Board = board;
        board.Background = background;

        GameObject go = Utils.CreateObject("@Tetromino");
        SingleTetromino tetromino= Utils.GetOrAddComponent<SingleTetromino>(go);
        board.Tetromino = tetromino;
        tetromino.Board = board;

        go.transform.parent = board.Root.transform;
        background.Root.transform.parent = board.Root.transform;

        return board;
    }
}