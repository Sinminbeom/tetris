using UnityEngine;

public class EnemyBoardFactory : IMultiBoardFactory
{

    public IBoard CreateBoard()
    {
        IBoard board = new EnemyBoard();
        board.Root = Utils.CreateObject("@EnemyBoard");

        IBackground background = new EnemyBackground();
        background.Board = board;
        board.Background = background;

        GameObject go = new GameObject("@Tetromino");
        EnemyTetromino tetromino = Utils.GetOrAddComponent<EnemyTetromino>(go);
        board.Tetromino = tetromino;
        tetromino.Board = board;

        go.transform.parent = board.Root.transform;
        background.Root.transform.parent = board.Root.transform;

        return board;
    }
}
