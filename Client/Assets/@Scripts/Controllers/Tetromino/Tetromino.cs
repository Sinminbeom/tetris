using Google.Protobuf.Protocol;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public IBoard Board { get; set; }

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public void Move(Vector3 pos, bool isRotate)
    {
        transform.position += pos;

        if (isRotate)
        {
            transform.rotation *= Quaternion.Euler(0, 0, 90);
        }
    }

    public void SyncMove(Vector3 pos, bool isRotate)
    {
        transform.position = pos;

        if (isRotate)
        {
            transform.rotation *= Quaternion.Euler(0, 0, 90);
        }
    }

    public void Spawn()
    {
        // 클라이언트에서 랜덤 생성 (My)
        ETetrominoType randomType = (ETetrominoType)Random.Range(0, 7);
        Spawn(randomType);

        // 서버 전송
        C_SpawnTetromino spawnTetromino = new C_SpawnTetromino();
        spawnTetromino.TetrominoType = randomType;
        Managers.Network.Send(spawnTetromino);
    }

    public void Spawn(ETetrominoType type)
    {
        // 서버로 부터 받아서 
        Transform tetrominoNode = this.transform;
        tetrominoNode.transform.rotation = Quaternion.identity;
        tetrominoNode.transform.position = Board.Pos + new Vector2(0, Board.halfHeight);

        Color32 color = Color.white;

        switch (type)
        {
            case ETetrominoType.I:
                color = new Color32(115, 251, 253, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-2f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;

            case ETetrominoType.J:
                color = new Color32(0, 33, 245, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1f), color, pooling: true);
                break;

            case ETetrominoType.L:
                color = new Color32(243, 168, 59, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1f), color, pooling: true);
                break;

            case ETetrominoType.O:
                color = new Color32(255, 253, 84, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1f), color, pooling: true);
                break;

            case ETetrominoType.S:
                color = new Color32(117, 250, 76, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;

            case ETetrominoType.T:
                color = new Color32(155, 47, 246, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                break;

            case ETetrominoType.Z:
                color = new Color32(235, 51, 35, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;
        }
    }
}