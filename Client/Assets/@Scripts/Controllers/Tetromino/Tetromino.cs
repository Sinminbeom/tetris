using UnityEngine;

public class Tetromino : BaseObject
{
    public IBoard Board { get; set; }
    protected override void Awake()
    {

    }

    protected override void Start()
    {

    }

    protected override void Update()
    {

    }

    public void Spawn()
    {
        Transform tetrominoNode = this.transform;

        tetrominoNode.transform.rotation = Quaternion.identity;
        tetrominoNode.transform.position = Board.Pos + new Vector2(0, Board.halfHeight);

        int index = Random.Range(0, 7);
        Color32 color = Color.white;

        // index = 0;

        switch (index)
        {
            // I : 하늘색
            case 0:
                color = new Color32(115, 251, 253, 255);
                GameObject go1 = Tile.CreateTile(tetrominoNode, new Vector2(-2f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                break;

            // J : 파란색
            case 1:
                color = new Color32(0, 33, 245, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color, pooling: true);
                break;

            // L : 귤색
            case 2:
                color = new Color32(243, 168, 59, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1.0f), color, pooling: true);
                break;

            // O : 노란색
            case 3:
                color = new Color32(255, 253, 84, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1f), color, pooling: true);
                break;

            // S : 녹색
            case 4:
                color = new Color32(117, 250, 76, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;

            // T : 자주색
            case 5:
                color = new Color32(155, 47, 246, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                break;

            // Z : 빨간색
            case 6:
                color = new Color32(235, 51, 35, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;
        }

        //return Tetromino;
    }
}