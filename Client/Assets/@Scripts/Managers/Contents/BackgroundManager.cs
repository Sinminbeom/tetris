using UnityEngine;

public abstract class BackgroundManager
{
    public abstract void LoadBackground();
}

public class SingleBackgroundManager : BackgroundManager
{
    public GameObject Root
    {
        get
        {
            return Utils.CreateObject("@Background");
        }
    }

    public override void LoadBackground()
    {
        int halfWidth = Managers.SingleBoard.halfWidth;
        int halfHeight = Managers.SingleBoard.halfHeight;

        Color color = Color.gray;

        // 타일 보드
        color.a = 0.5f;
        for (int x = -halfWidth; x < halfWidth; ++x)
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
            Tile.CreateTile(Root.transform, new Vector2(-halfWidth - 1, y), color, 0);
            Tile.CreateTile(Root.transform, new Vector2(halfWidth, y), color, 0);
        }

        // 아래 테두리
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            Tile.CreateTile(Root.transform, new Vector2(x, -halfHeight), color, 0);
        }
    }
}

public class MultiBackgroundManager : BackgroundManager
{
    public GameObject MyRoot
    {
        get
        {
            return Utils.CreateObject("@MyBackground");
        }
    }

    public GameObject EnemyRoot
    {
        get
        {
            return Utils.CreateObject("@EnemyBackground");
        }
    }

    public override void LoadBackground()
    {
        LoadMyBackground();
        LoadEnemyBackground();
    }

    private void LoadMyBackground()
    {
        Color color = Color.cyan;

        Vector2Int myBoardPos = Managers.MyBoard.myBoardPos;

        int halfWidth = Managers.MyBoard.halfWidth;
        int halfHeight = Managers.MyBoard.halfHeight;

        Tile.CreateTile(MyRoot.transform, myBoardPos, color, 0);

        int minWidth = myBoardPos.x - halfWidth;

        color.a = 0.5f;
        for (int x = minWidth; x < -halfWidth; ++x)
        {
            for (int y = halfHeight; y > -halfHeight; --y)
            {
                Tile.CreateTile(MyRoot.transform, new Vector2(x, y), color, 0);
            }
        }

        // 좌우 테두리
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            Tile.CreateTile(MyRoot.transform, myBoardPos + new Vector2(-halfWidth - 1, y), color, 0);
            Tile.CreateTile(MyRoot.transform, myBoardPos + new Vector2(halfWidth, y), color, 0);
        }

        // 아래 테두리
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            Tile.CreateTile(MyRoot.transform, myBoardPos + new Vector2(x, -halfHeight), color, 0);
        }
    }

    private void LoadEnemyBackground()
    {
        Color color = Color.red;

        Vector2Int enemyBoardPos = Managers.EnemyBoard.enemyBoardPos;

        int halfWidth = Managers.EnemyBoard.halfWidth;
        int halfHeight = Managers.EnemyBoard.halfHeight;

        Tile.CreateTile(EnemyRoot.transform, enemyBoardPos, color, 0);

        // 5 + 10 = 15
        // 5
        int minWidth = halfWidth + enemyBoardPos.x;

        // 타일 보드
        color.a = 0.5f;
        for (int x = halfWidth; x < minWidth; ++x)
        {
            for (int y = halfHeight; y > -halfHeight; --y)
            {
                Tile.CreateTile(EnemyRoot.transform, new Vector2(x, y), color, 0);
            }
        }

        // 좌우 테두리
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            Tile.CreateTile(EnemyRoot.transform, enemyBoardPos + new Vector2(-halfWidth - 1, y), color, 0);
            Tile.CreateTile(EnemyRoot.transform, enemyBoardPos + new Vector2(halfWidth, y), color, 0);
        }

        // 아래 테두리
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            Tile.CreateTile(EnemyRoot.transform, enemyBoardPos + new Vector2(x, -halfHeight), color, 0);
        }
    }
}