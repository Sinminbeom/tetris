using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public abstract class abBoard : IBoard
{
    public int boardWidth { get; protected set; } = 10;
    public int boardHeight { get; protected set; } = 20;
    public float FallCycle { get; protected set; } = 1.0f;

    public int halfWidth { get; protected set; }
    public int halfHeight { get; protected set; }

    public float NextFallTime { get; set; }

    public Tile[,] _tiles;

    public GameObject Root { get; set; }

    public Vector2Int Pos { get; set; } = Vector2Int.zero;
    public IBackground Background { get; set; }
    public Tetromino Tetromino { get; set; }

    public abBoard()
    {
        _tiles = new Tile[boardWidth, boardHeight];

        this.halfWidth = Mathf.RoundToInt(this.boardWidth * 0.5f);
        this.halfHeight = Mathf.RoundToInt(this.boardHeight * 0.5f);

        NextFallTime = Time.time + FallCycle;
    }

    public abstract void Init();

    public void Spawn()
    {
        Tetromino.Spawn();
    }

    // 이동 가능한지 체크
    // 완료
    public bool CanMove()
    {
        for (int i = 0; i < Tetromino.transform.childCount; ++i)
        {
            var node = Tetromino.transform.GetChild(i);
            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth - Pos.x);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            // 왼쪽 오른쪽 벽 체크
            if (x < 0 || x > boardWidth - 1)
                return false;

            // 바닥 체크
            if (y < 0)
                return false;

            // 블록 충돌 체크
            try
            {
                Vector2Int checkPos = new Vector2Int(x, y);
                if (_tiles[checkPos.x, checkPos.y] != null)
                    return false;
            } catch { }
        }

        return true;
    }

    // 완료
    public bool MoveTo(Vector3 pos, bool isRotate)
    {
        Vector3 oldPos = Tetromino.transform.position;
        Quaternion oldRot = Tetromino.transform.rotation;

        Tetromino.Move(pos, isRotate);

        if (!CanMove())
        {
            Tetromino.transform.position = oldPos;
            Tetromino.transform.rotation = oldRot;

            return false;
        }

        return true;
    }

    // 테트로미노를 보드에 추가
    // 완료
    public void AddObject()
    {
        Transform root = Tetromino.gameObject.transform;
        while (root.childCount > 0)
        {
            Transform node = root.GetChild(0);

            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth - Pos.x);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            Vector2Int pos = new Vector2Int(x, y);
            _tiles[pos.x, pos.y] = node.GetComponent<Tile>();

            node.parent = Root.transform;
        }
    }

    public void CheckCompleteRow()
    {
        for (int y = 0; y < boardHeight; y++)
        {
            bool fullRow = true;

            // 해당 행이 다 차 있는지 확인
            for (int x = 0; x < boardWidth; x++)
            {
                if (_tiles[x, y] == null)
                {
                    fullRow = false;
                    break;
                }
            }

            if (fullRow)
            {
                // 행 제거
                for (int x = 0; x < boardWidth; x++)
                {
                    if (_tiles[x, y] != null)
                    {
                        Managers.Resource.Destroy(_tiles[x, y].gameObject);
                        _tiles[x, y] = null;
                    }
                }

                // 위에 있는 행들을 아래로 한 칸씩 내림
                for (int yy = y + 1; yy < boardHeight; yy++)
                {
                    for (int x = 0; x < boardWidth; x++)
                    {
                        if (_tiles[x, yy] != null)
                        {
                            _tiles[x, yy - 1] = _tiles[x, yy];
                            _tiles[x, yy] = null;
                            _tiles[x, yy - 1].transform.position += new Vector3(0, -1, 0);
                        }
                    }
                }

                y--;
            }
        }
    }
}
