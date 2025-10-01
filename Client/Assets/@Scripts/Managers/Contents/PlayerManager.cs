using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public PlayerInfo PlayerInfo { get; private set; }
    public void Load(PlayerInfo playerInfo)
    {
        PlayerInfo = playerInfo;
    }

}
