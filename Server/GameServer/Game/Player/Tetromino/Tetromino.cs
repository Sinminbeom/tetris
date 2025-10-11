using Google.Protobuf.Protocol;

namespace GameServer
{
    public class Tetromino
    {
        public ETetrominoType type { get; set; }
        public PositionInfo positionInfo { get; set; } = new PositionInfo();

    }
}
