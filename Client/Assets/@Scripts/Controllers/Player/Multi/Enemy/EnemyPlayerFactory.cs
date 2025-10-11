using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EnemyPlayerFactory : IMultiPlayerFactory
{
    private IMultiPlayerInfoFactory multiPlayerInfoFactory;

    public EnemyPlayerFactory(IMultiPlayerInfoFactory multiPlayerInfoFactory)
    {
        this.multiPlayerInfoFactory = multiPlayerInfoFactory;
    }

    public IPlayer CreatePlayer(PlayerInfo playerInfo)
    {
        IPlayer player = new EnemyPlayer();

        IBoard board = this.multiPlayerInfoFactory.CreateBoard();
        player.Board = board;
        player.PlayerInfo = playerInfo;

        return player;
    }
}
