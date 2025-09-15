using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoard : abMultiBoard
{
    protected int leftX;

    public MyBoard()
    {
        this.leftX = Mathf.RoundToInt(-this.camWidth * 0.5f);
        Pos = new Vector2Int(leftX + Mathf.RoundToInt(camWidth * 0.25f), 0);

    }

    public override void Init()
    {
        Background.Init();
    }
}
