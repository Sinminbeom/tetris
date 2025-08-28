using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundManager
{
    public GameObject Root
    {
        get
        {
            return Utils.CreateObject("@Background");
        }
    }
    public void LoadBackground()
    {
        int halfWidth = Managers.Board.halfWidth;
        int halfHeight = Managers.Board.halfHeight;

        Color color = Color.gray;

        // Ÿ�� ����
        color.a = 0.5f;
        for (int x = -halfWidth; x < halfWidth; ++x)
        {
            for (int y = halfHeight; y > -halfHeight; --y)
            {
                Tile.CreateTile(Root.transform, new Vector2(x, y), color, 0);
            }
        }

        // �¿� �׵θ�
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            Tile.CreateTile(Root.transform, new Vector2(-halfWidth - 1, y), color, 0);
            Tile.CreateTile(Root.transform, new Vector2(halfWidth, y), color, 0);
        }

        // �Ʒ� �׵θ�
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            Tile.CreateTile(Root.transform, new Vector2(x, -halfHeight), color, 0);
        }
    }

}
