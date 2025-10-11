using UnityEngine;

public class MyBoardFactory : IMultiBoardFactory
{

    public IBoard CreateBoard()
    {
        IBoard board = new MyBoard();
        board.Root = Utils.CreateObject("@MyBoard");

        IBackground background = new MyBackground();
        background.Board = board;
        board.Background = background;

        GameObject go = new GameObject("@MyTetromino");
        MyTetromino tetromino = Utils.GetOrAddComponent<MyTetromino>(go);
        board.Tetromino = tetromino;
        tetromino.Board = board;

        go.transform.parent = board.Root.transform;
        background.Root.transform.parent = board.Root.transform;

        return board;
    }
}
