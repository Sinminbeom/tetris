using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoard : abMultiBoard
{
    protected int rightX;

    public EnemyBoard()
    {
        this.rightX = Mathf.RoundToInt(this.camWidth * 0.5f);
        Pos = new Vector2Int(rightX - Mathf.RoundToInt(camWidth * 0.25f), 0);
    }

    public override void Init()
    {
        Background.Init();
    }
}
