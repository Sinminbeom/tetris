using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoard : abMultiBoard
{

    public EnemyBoard()
    {
        Pos = new Vector2Int(15, 0);
    }

    public override void Init()
    {
        Background.Init();
    }
}
