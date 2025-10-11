using System.Numerics;

namespace GameServer
{
    public class Board
    {
        public int boardWidth { get; protected set; } = 10;
        public int boardHeight { get; protected set; } = 20;

        public bool[,] _tiles;
        public Tetromino Tetromino { get; protected set; } = new Tetromino();

        public Board()
        {
            _tiles = new bool[boardWidth, boardHeight];
        }

        // 이동 가능한지 체크
        // 완료
        public bool CanMove()
        {
            //for (int i = 0; i < Tetromino.transform.childCount; ++i)
            //{
            //    var node = Tetromino.transform.GetChild(i);
            //    int x = Mathf.RoundToInt(node.transform.position.x + halfWidth - Pos.x);
            //    int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            //    // 왼쪽 오른쪽 벽 체크
            //    if (x < 0 || x > boardWidth - 1)
            //        return false;

            //    // 바닥 체크
            //    if (y < 0)
            //        return false;

            //    // 블록 충돌 체크
            //    try
            //    {
            //        Vector2Int checkPos = new Vector2Int(x, y);
            //        if (_tiles[checkPos.x, checkPos.y] != null)
            //            return false;
            //    }
            //    catch { }
            //}

            return true;
        }

        // 완료
        public bool MoveTo(Vector3 pos, bool isRotate)
        {
            //Vector3 oldPos = Tetromino.transform.position;
            //Quaternion oldRot = Tetromino.transform.rotation;

            //Tetromino.Move(pos, isRotate);

            //if (!CanMove())
            //{
            //    Tetromino.transform.position = oldPos;
            //    Tetromino.transform.rotation = oldRot;

            //    return false;
            //}

            return true;
        }

        // 테트로미노를 보드에 추가
        // 완료
        public void AddObject()
        {
            //Transform root = Tetromino.gameObject.transform;
            //while (root.childCount > 0)
            //{
            //    Transform node = root.GetChild(0);

            //    int x = Mathf.RoundToInt(node.transform.position.x + halfWidth - Pos.x);
            //    int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            //    Vector2Int pos = new Vector2Int(x, y);
            //    _tiles[pos.x, pos.y] = node.GetComponent<Tile>();

            //    node.parent = Root.transform;
            //}
        }
    }
}