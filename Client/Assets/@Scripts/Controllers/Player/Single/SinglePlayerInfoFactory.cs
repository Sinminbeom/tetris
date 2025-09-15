using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SinglePlayerInfoFactory : IPlayerInfoFactory
{
    public IBoard CreateBoard()
    {
        IBoardFactory boardFactory = new SingleBoardFactory();
        IBoard board = boardFactory.CreateBoard();
        return board;
    }
}
