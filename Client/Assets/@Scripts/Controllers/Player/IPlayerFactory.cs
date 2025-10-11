using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IPlayerFactory
{
    IPlayer CreatePlayer(PlayerInfo playerInfo);
}