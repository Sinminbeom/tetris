using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abStage : MonoBehaviour
{
    [Header("Editor Objects")]
    public GameObject tilePrefab;
    public Transform backgroundNode;
    public Transform boardNode;
    public Transform tetrominoNode;
    public GameObject gameoverPanel;

    [Header("Game Settings")]
    [Range(4, 40)]
    public int boardWidth = 10;
    [Range(5, 20)]
    public int boardHeight = 20;
    public float fallCycle = 1.0f;

    protected int halfWidth;
    protected int halfHeight;

    protected float nextFallTime;

    protected float camHeight;
    protected float camWidth;

    protected float leftX;
    protected float rightX;

    // Start is called before the first frame update
    protected virtual void Start()
    {

        gameoverPanel.SetActive(false);

        halfWidth = Mathf.RoundToInt(boardWidth * 0.5f);
        halfHeight = Mathf.RoundToInt(boardHeight * 0.5f);

        this.camHeight = Camera.main.orthographicSize * 2f;
        this.camWidth = camHeight * Camera.main.aspect;

        this.leftX = Mathf.RoundToInt(-this.camWidth * 0.5f);
        this.rightX = Mathf.RoundToInt(this.camWidth * 0.5f);

        CreateBackground();
        CreateColumns();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected abstract void CreateBackground();
    protected abstract void CreateColumns();
    protected abstract void CreateTetromino();

    // 타일 생성
    public Tile CreateTile(Transform parent, Vector2 position, Color color, int order = 1)
    {
        GameObject go = Instantiate(tilePrefab);
        go.transform.parent = parent;
        go.transform.localPosition = position;

        Tile tile = go.GetComponent<Tile>();
        tile.color = color;
        tile.sortingOrder = order;

        return tile;
    }
}
