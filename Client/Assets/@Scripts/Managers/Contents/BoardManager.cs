using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    public int boardWidth { get; private set; } = 10;
    public int boardHeight { get; private set; } = 20;

    public float fallCycle { get; private set; } = 1.0f;

    public int halfWidth { get; private set; }
    public int halfHeight { get; private set; }

    public float nextFallTime { get; set; }
    public Dictionary<Vector2Int, Tile> _tiles = new Dictionary<Vector2Int, Tile>();

    public GameObject Root
    {
        get
        {
            return Utils.CreateObject("@Board");
        }
    }

    public void LoadBoard()
    {
        this.halfWidth = Mathf.RoundToInt(this.boardWidth * 0.5f);
        this.halfHeight = Mathf.RoundToInt(this.boardHeight * 0.5f);

        nextFallTime = Time.time + fallCycle;

    }

    // 이동 가능한지 체크
    public bool CanMove(Tetromino tetromino)
    {
        for (int i = 0; i < tetromino.transform.childCount; ++i)
        {
            var node = tetromino.transform.GetChild(i);
            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
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

    public bool MoveTo(Tetromino tetromino, Vector3 pos, bool isRotate)
    {
        Vector3 oldPos = tetromino.transform.position;
        Quaternion oldRot = tetromino.transform.rotation;

        tetromino.Move(pos, isRotate);

        if (!CanMove(tetromino))
        {
            tetromino.transform.position = oldPos;
            tetromino.transform.rotation = oldRot;

            return false;
        }

        return true;
    }

    // 테트로미노를 보드에 추가
    public void AddObject(Transform root)
    {
        while (root.childCount > 0)
        {
            Transform node = root.GetChild(0);

            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            Vector2Int pos = new Vector2Int(x, y);
            _tiles[pos] = node.GetComponent<Tile>();

            node.parent = Root.transform;
        }
    }

    public void CheckCompleteRow()
    {
        bool isCleared = false;

        int cnt = 0;

        for (int y = 0; y < Managers.Board.boardHeight; y++)
        {
            for (int x = 0; x < Managers.Board.boardWidth; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (_tiles.ContainsKey(pos) && _tiles[pos] != null)
                {
                    cnt++;
                }
            }

            if (cnt == Managers.Board.boardWidth)
            {
                DestroyRow(y);
                isCleared = true;
            }

            cnt = 0;
        }

        if (isCleared)
        {
            for(int y = 1; y < Managers.Board.boardHeight; ++y)
            {
                if (GetChildRowCount(y) == 0)
                    continue;

                int emptyY = 0;
                int j = y - 1;
                while (j >= 0)
                {
                    if(GetChildRowCount(j) == 0)
                    {
                        emptyY++;
                    }
                    j--;
                }

                if (emptyY > 0)
                {
                    int targetY = y - emptyY;

                    for (int x = 0; x < Managers.Board.boardWidth; ++x)
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

    public int GetChildRowCount(int y)
    {
        int cnt = 0;
        for (int x = 0; x < Managers.Board.boardWidth; x++)
        {
            Vector2Int pos = new Vector2Int(x, y);
            if (_tiles.ContainsKey(pos) && _tiles[pos] != null)
            {
                cnt++;
            }
        }
        return cnt;
    }

    public void DestroyRow(int y)
    {
        for (int x = 0; x < Managers.Board.boardWidth; x++)
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
