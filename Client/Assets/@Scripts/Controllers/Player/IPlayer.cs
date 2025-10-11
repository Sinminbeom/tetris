using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IPlayer
{
    void Init();
    PlayerInfo PlayerInfo { get; set; }
    public IBoard Board { get; set; }
}
