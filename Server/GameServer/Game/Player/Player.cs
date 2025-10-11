using Google.Protobuf.Protocol;
using Server;
using ServerCore;

namespace GameServer
{
    public class Player
    {
        public ClientSession Session { get; set; }
        public PlayerInfo PlayerInfo { get; set; } = new PlayerInfo();
        public Board Board { get; set; } = new Board();
        public GameRoom Room { get; set; }
    }

}