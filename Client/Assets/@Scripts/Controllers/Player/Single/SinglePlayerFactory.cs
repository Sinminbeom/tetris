using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SinglePlayerFactory : IPlayerFactory
{
    private IPlayerInfoFactory playerInfoFactory;

    public SinglePlayerFactory(IPlayerInfoFactory playerInfoFactory)
    {
        this.playerInfoFactory = playerInfoFactory;
    }
    public IPlayer CreatePlayer()
    {
        IPlayer player = new SinglePlayer();

        IBoard board = this.playerInfoFactory.CreateBoard();
        player.Board = board;

        return player;
    }
}