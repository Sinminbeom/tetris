using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : abMultiPlayer
{
    public MyPlayer()
    {
    }

    public override void Init()
    {
        Board.Init();
        Board.Spawn();
    }
}