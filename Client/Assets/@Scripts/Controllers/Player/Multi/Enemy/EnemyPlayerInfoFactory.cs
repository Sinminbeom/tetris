using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EnemyPlayerInfoFactory : IMultiPlayerInfoFactory
{

    public IBoard CreateBoard()
    {
        IBoardFactory boardFactory = new EnemyBoardFactory();
        IBoard board = boardFactory.CreateBoard();
        return board;
    }
}
