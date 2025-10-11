using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoard : abMultiBoard
{

    public MyBoard()
    {
        Pos = new Vector2Int(-15, 0);
    }

    public override void Init()
    {
        Background.Init();
    }
}
