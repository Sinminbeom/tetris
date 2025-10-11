using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackground : MultiBackground
{

    public override void Init()
    {
        Color color = Color.red;

        Vector2Int enemyBoardPos = Board.Pos;

        int halfWidth = ((EnemyBoard)Board).halfWidth;
        int halfHeight = ((EnemyBoard)Board).halfHeight;

        Tile.CreateTile(Root.transform, enemyBoardPos, color, 0);

        // 5 + 10 = 15
        // 5
        int minWidth = halfWidth + enemyBoardPos.x;

        // 타일 보드
        color.a = 0.5f;
        for (int x = enemyBoardPos.x - halfWidth; x < minWidth; ++x)
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
            Tile.CreateTile(Root.transform, enemyBoardPos + new Vector2(-halfWidth - 1, y), color, 0);
            Tile.CreateTile(Root.transform, enemyBoardPos + new Vector2(halfWidth, y), color, 0);
        }

        // 아래 테두리
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            Tile.CreateTile(Root.transform, enemyBoardPos + new Vector2(x, -halfHeight), color, 0);
        }

    }
}
