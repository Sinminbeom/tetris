using System.Collections.Generic;
using UnityEngine;

public abstract class abBoard : IBoard
{
    public int boardWidth { get; protected set; } = 10;
    public int boardHeight { get; protected set; } = 20;
    public float FallCycle { get; protected set; } = 1.0f;

    public int halfWidth { get; protected set; }
    public int halfHeight { get; protected set; }

    public float NextFallTime { get; set; }
    public Dictionary<Vector2Int, Tile> _tiles = new Dictionary<Vector2Int, Tile>();

    public GameObject Root { get; set; }

    public Vector2Int Pos { get; set; } = Vector2Int.zero;
    public IBackground Background { get; set; }
    public Tetromino Tetromino { get; set; }

    public abBoard()
    {
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
            Vector2Int checkPos = new Vector2Int(x, y);
            if (_tiles.ContainsKey(checkPos) && _tiles[checkPos] != null)
                return false;
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
            _tiles[pos] = node.GetComponent<Tile>();

            node.parent = Root.transform;
        }
    }

    // 완료
    public void CheckCompleteRow()
    {
        bool isCleared = false;

        int cnt = 0;

        for (int y = 0; y < this.boardHeight; y++)
        {
            for (int x = 0; x < this.boardWidth; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (_tiles.ContainsKey(pos) && _tiles[pos] != null)
                {
                    cnt++;
                }
            }

            if (cnt == this.boardWidth)
            {
                DestroyRow(y);
                isCleared = true;
            }

            cnt = 0;
        }

        if (isCleared)
        {
            for (int y = 1; y < this.boardHeight; ++y)
            {
                if (GetChildRowCount(y) == 0)
                    continue;

                int emptyY = 0;
                int j = y - 1;
                while (j >= 0)
                {
                    if (GetChildRowCount(j) == 0)
                    {
                        emptyY++;
                    }
                    j--;
                }

                if (emptyY > 0)
                {
                    int targetY = y - emptyY;

                    for (int x = 0; x < this.boardWidth; ++x)
                    {
                        Vector2Int dstPos = new Vector2Int(x, targetY);
                        Vector2Int srcPos = new Vector2Int(x, y);

                        if (_tiles.ContainsKey(srcPos) && _tiles[srcPos] != null)
                        {
                            _tiles[srcPos].transform.position += new Vector3(0, -emptyY, 0);
                            _tiles[dstPos] = _tiles[srcPos];
                            _tiles[srcPos] = null;
                        }
                    }
                }
            }
        }
    }

    // 그냥 써도 됌
    public int GetChildRowCount(int y)
    {
        int cnt = 0;
        for (int x = 0; x < this.boardWidth; x++)
        {
            Vector2Int pos = new Vector2Int(x, y);
            if (_tiles.ContainsKey(pos) && _tiles[pos] != null)
            {
                cnt++;
            }
        }
        return cnt;
    }

    // 그냥 써도 됌
    public void DestroyRow(int y)
    {
        for (int x = 0; x < this.boardWidth; x++)
        {
            Vector2Int pos = new Vector2Int(x, y);
            if (_tiles.ContainsKey(pos) && _tiles[pos] != null)
            {
                Managers.Resource.Destroy(_tiles[pos].gameObject);
                _tiles[pos] = null;
            }
        }
    }
}
