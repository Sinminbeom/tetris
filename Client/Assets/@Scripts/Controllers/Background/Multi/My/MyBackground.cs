using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MyBackground : MultiBackground
{

    public override void Init()
    {
        Color color = Color.cyan;
        Vector2Int myBoardPos = Board.Pos;

        int halfWidth = Board.halfWidth;
        int halfHeight = Board.halfHeight;

        Tile.CreateTile(Root.transform, myBoardPos, color, 0);

        int minWidth = myBoardPos.x - halfWidth;

        color.a = 0.5f;
        for (int x = minWidth; x < myBoardPos.x + halfWidth; ++x)
        {
            for (int y = halfHeight; y > -halfHeight; --y)
            {
                Tile.CreateTile(Root.transform, new Vector2(x, y), color, 0);
            }
        }

        // 좌우 테두리
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            Tile.CreateTile(Root.transform, myBoardPos + new Vector2(-halfWidth - 1, y), color, 0);
            Tile.CreateTile(Root.transform, myBoardPos + new Vector2(halfWidth, y), color, 0);
        }

        // 아래 테두리
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            Tile.CreateTile(Root.transform, myBoardPos + new Vector2(x, -halfHeight), color, 0);
        }

    }
}
