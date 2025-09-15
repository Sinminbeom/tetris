using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : abPlayer
{
    public SinglePlayer()
    {
    }

    public override void Init()
    {
        Board.Init();
    }
}
