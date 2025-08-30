using UnityEngine;

public class EnemyStage : abStage
{
    Vector2 enemyBoardPos;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
    }

    // 배경 타일을 생성
    protected override void CreateBackground()
    {
        Color color = Color.red;
        
        enemyBoardPos = new Vector2(rightX - Mathf.RoundToInt(camWidth * 0.25f), 0f);

        CreateTile(backgroundNode, enemyBoardPos, color, 0);

        // 5 + 10 = 15
        // 5
        int width = halfWidth + (int)enemyBoardPos.x;

        // 타일 보드
        color.a = 0.5f;
        for (int x = halfWidth; x < width; ++x)
        {
            for (int y = halfHeight; y > -halfHeight; --y)
            {
                CreateTile(backgroundNode, new Vector2(x, y), color, 0);
            }
        }

        // 좌우 테두리
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            CreateTile(backgroundNode, enemyBoardPos + new Vector2(-halfWidth - 1, y), color, 0);
            CreateTile(backgroundNode, enemyBoardPos + new Vector2(halfWidth, y), color, 0);
        }

        // 아래 테두리
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            CreateTile(backgroundNode, enemyBoardPos + new Vector2(x, -halfHeight), color, 0);
        }
    }

    protected override void CreateColumns()
    {
        for (int i = 0; i < boardHeight; ++i)
        {
            GameObject col = new GameObject((boardHeight - i - 1).ToString());
            col.transform.position = (Vector3) enemyBoardPos + new Vector3(0, halfHeight - i, 0);
            col.transform.parent = boardNode;
        }
    }

    protected override void CreateTetromino()
    {
        int index = Random.Range(0, 7);
        Color32 color = Color.white;

        tetrominoNode.rotation = Quaternion.identity;
        tetrominoNode.position = new Vector2(0, halfHeight);

        switch (index)
        {
            // I : 하늘색
            case 0:
                color = new Color32(115, 251, 253, 255);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(-2f, 0.0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(1f, 0.0f), color);
                break;

            // J : 파란색
            case 1:
                color = new Color32(0, 33, 245, 255);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(-1f, 1.0f), color);
                break;

            // L : 귤색
            case 2:
                color = new Color32(243, 168, 59, 255);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(1f, 1.0f), color);
                break;

            // O : 노란색
            case 3:
                color = new Color32(255, 253, 84, 255);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(1f, 0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, 1f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(1f, 1f), color);
                break;

            // S : 녹색
            case 4:
                color = new Color32(117, 250, 76, 255);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(-1f, -1f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, -1f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(1f, 0f), color);
                break;

            // T : 자주색
            case 5:
                color = new Color32(155, 47, 246, 255);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(-1f, 0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(1f, 0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, 1f), color);
                break;

            // Z : 빨간색
            case 6:
                color = new Color32(235, 51, 35, 255);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(-1f, 1f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, 1f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, enemyBoardPos + new Vector2(1f, 0f), color);
                break;
        }
    }
}
