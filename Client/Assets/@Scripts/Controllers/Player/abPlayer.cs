using Google.Protobuf.Protocol;
using UnityEngine;

public abstract class abPlayer : IPlayer
{
    public PlayerInfo PlayerInfo { get; set; } = new PlayerInfo();
    public IBoard Board { get; set; }

    public abPlayer()
    {
    }

    public abstract void Init();
}
