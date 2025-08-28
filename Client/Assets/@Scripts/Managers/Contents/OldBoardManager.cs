using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldBoardManager
{
    public int boardWidth { get; private set; } = 10;
    public int boardHeight { get; private set; } = 20;

    public float fallCycle { get; private set; } = 1.0f;

    public int halfWidth { get; private set; }
    public int halfHeight { get; private set; }

    public float nextFallTime { get; set; }

    public GameObject board;

    public void Init()
    {
        this.halfWidth = Mathf.RoundToInt(this.boardWidth * 0.5f);
        this.halfHeight = Mathf.RoundToInt(this.boardHeight * 0.5f);

        nextFallTime = Time.time + fallCycle;

        CreateColumns();
    }

    public void CreateColumns()
    {
        this.board = Utils.CreateObject("Board");
        for (int i = 0; i < this.boardHeight; ++i)
        {
            GameObject col = new GameObject((this.boardHeight - i - 1).ToString());
            col.transform.position = new Vector3(0, this.halfHeight - i, 0);
            col.transform.parent = this.board.transform;
        }
    }

    // 이동 가능한지 체크
    public bool CanMoveTo(Transform root)
    {
        for (int i = 0; i < root.childCount; ++i)
        {
            var node = root.GetChild(i);
            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            if (x < 0 || x > boardWidth - 1)
                return false;

            if (y < 0)
                return false;

            var column = board.transform.Find(y.ToString());

            if (column != null && column.Find(x.ToString()) != null)
                return false;

        }

        return true;
    }

    // 테트로미노를 보드에 추가
    public void AddToBoard(Transform root)
    {
        while (root.childCount > 0)
        {
            Transform node = root.GetChild(0);

            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            node.parent = board.transform.Find(y.ToString());
            node.name = x.ToString();
        }
    }

    // 보드에 완성된 행이 있으면 삭제
    public void CheckBoardColumn()
    {
        bool isCleared = false;

        Transform boardNode = board.transform;

        foreach (Transform column in boardNode)
        {
            if (column.childCount == boardWidth)
            {
                foreach (Transform tile in column)
                {
                    Debug.Log(tile.name);
                    tile.name = "Tile";
                    Managers.Resource.Destroy(tile.gameObject);
                }
                column.DetachChildren();
                isCleared = true;
            }
        }

        if (isCleared)
        {
            for (int i = 1; i < boardNode.childCount; ++i)
            {
                var column = boardNode.Find(i.ToString());

                // 이미 비어 있는 행은 무시
                if (column.childCount == 0)
                    continue;

                int emptyCol = 0;
                int j = i - 1;
                while (j >= 0)
                {
                    if (boardNode.Find(j.ToString()).childCount == 0)
                    {
                        emptyCol++;
                    }
                    j--;
                }

                if (emptyCol > 0)
                {
                    var targetColumn = boardNode.Find((i - emptyCol).ToString());

                    while (column.childCount > 0)
                    {
                        Transform tile = column.GetChild(0);
                        Debug.Log($"emptyCol : {tile.name}");
                        tile.parent = targetColumn;
                        tile.transform.position += new Vector3(0, -emptyCol, 0);
                    }
                    column.DetachChildren();
                }
            }
        }
    }
}
