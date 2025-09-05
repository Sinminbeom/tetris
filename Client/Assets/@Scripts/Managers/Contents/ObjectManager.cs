using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

/*
public class ObjectManager
{
    public Tetromino Tetromino { get; set; }
    public int tetrominoObjectId { get; } = 0;
    Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

    #region Roots

    public GameObject TetrominoRoot { get { return Utils.CreateObject("@Tetromino"); } }

    #endregion

    public void LoadTetromino()
    {
        CreateTetromino();
        Spawn();
    }

    public void CreateTetromino()
    {
        _objects[tetrominoObjectId] = TetrominoRoot;

        Tetromino = Utils.GetOrAddComponent<Tetromino>(TetrominoRoot);
        Tetromino.ObjectId = tetrominoObjectId;
    }

    public Tetromino Spawn()
    {
        GameObject go = FindById(tetrominoObjectId);
        Transform tetrominoNode = go.transform;

        tetrominoNode.transform.rotation = Quaternion.identity;
        tetrominoNode.transform.position = new Vector2(0, Managers.SingleBoard.halfHeight);

        int index = Random.Range(0, 7);
        Color32 color = Color.white;

        // index = 0;

        switch (index)
        {
            // I : ÇÏ´Ã»ö
            case 0:
                color = new Color32(115, 251, 253, 255);
                GameObject go1 = Tile.CreateTile(tetrominoNode, new Vector2(-2f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                break;

            // J : ÆÄ¶õ»ö
            case 1:
                color = new Color32(0, 33, 245, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color, pooling: true);
                break;

            // L : ±Ö»ö
            case 2:
                color = new Color32(243, 168, 59, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1.0f), color, pooling: true);
                break;

            // O : ³ë¶õ»ö
            case 3:
                color = new Color32(255, 253, 84, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1f), color, pooling: true);
                break;

            // S : ³ì»ö
            case 4:
                color = new Color32(117, 250, 76, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;

            // T : ÀÚÁÖ»ö
            case 5:
                color = new Color32(155, 47, 246, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                break;

            // Z : »¡°£»ö
            case 6:
                color = new Color32(235, 51, 35, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;
        }

        return Tetromino;
    }

    public GameObject FindById(int id)
    {
        GameObject go = null;
        _objects.TryGetValue(id, out go);
        return go;
    }

}
*/

public abstract class ObjectManager
{

    protected Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

    public void LoadTetromino()
    {
        CreateTetromino();
        Spawn();
    }

    public abstract void CreateTetromino();

    public abstract MyTetromino Spawn();

    public GameObject FindById(int id)
    {
        GameObject go = null;
        _objects.TryGetValue(id, out go);
        return go;
    }


}

public class SingleObjectManager : ObjectManager
{
    // single
    public MyTetromino SingleTetromino { get; set; }

    #region Roots
    public GameObject SingleTetrominoRoot { get { return Utils.CreateObject("@SingleTetromino"); } }
    #endregion

    public override void CreateTetromino()
    {
        _objects[(int)EObjectType.SingleTetromino] = SingleTetrominoRoot;

        SingleTetromino = Utils.GetOrAddComponent<MyTetromino>(SingleTetrominoRoot);
        SingleTetromino.ObjectId = (int)EObjectType.SingleTetromino;
    }

    public override MyTetromino Spawn()
    {
        GameObject go = FindById((int)EObjectType.SingleTetromino);
        Transform tetrominoNode = go.transform;

        tetrominoNode.transform.rotation = Quaternion.identity;
        tetrominoNode.transform.position = new Vector2(0, Managers.SingleBoard.halfHeight);

        int index = Random.Range(0, 7);
        Color32 color = Color.white;

        switch (index)
        {
            // I : ÇÏ´Ã»ö
            case 0:
                color = new Color32(115, 251, 253, 255);
                GameObject go1 = Tile.CreateTile(tetrominoNode, new Vector2(-2f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                break;

            // J : ÆÄ¶õ»ö
            case 1:
                color = new Color32(0, 33, 245, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color, pooling: true);
                break;

            // L : ±Ö»ö
            case 2:
                color = new Color32(243, 168, 59, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1.0f), color, pooling: true);
                break;

            // O : ³ë¶õ»ö
            case 3:
                color = new Color32(255, 253, 84, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1f), color, pooling: true);
                break;

            // S : ³ì»ö
            case 4:
                color = new Color32(117, 250, 76, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;

            // T : ÀÚÁÖ»ö
            case 5:
                color = new Color32(155, 47, 246, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                break;

            // Z : »¡°£»ö
            case 6:
                color = new Color32(235, 51, 35, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;
        }

        return SingleTetromino;
    }

}

public class MultiObjectManager : ObjectManager
{
    // multi
    public MyTetromino MyTetromino { get; set; }
    public Tetromino EnemyTetromino { get; set; }

    #region Roots
    public GameObject MyTetrominoRoot { get { return Utils.CreateObject("@MyTetromino"); } }
    public GameObject EnemyTetrominoRoot { get { return Utils.CreateObject("@EnemyTetromino"); } }
    #endregion

    public override void CreateTetromino()
    {
        _objects[(int)EObjectType.MyTetromino] = MyTetrominoRoot;

        MyTetromino = Utils.GetOrAddComponent<MyTetromino>(MyTetrominoRoot);
        MyTetromino.ObjectId = (int)EObjectType.MyTetromino;

        _objects[(int)EObjectType.EnemyTetromino] = EnemyTetrominoRoot;

        EnemyTetromino = Utils.GetOrAddComponent<Tetromino>(EnemyTetrominoRoot);
        EnemyTetromino.ObjectId = (int)EObjectType.EnemyTetromino;
    }

    public override MyTetromino Spawn()
    {
        GameObject go = FindById((int)EObjectType.MyTetromino);
        Transform tetrominoNode = go.transform;

        Vector2Int myBoardPos = Managers.MyBoard.myBoardPos;

        tetrominoNode.transform.rotation = Quaternion.identity;
        tetrominoNode.transform.position = myBoardPos + new Vector2(0, Managers.MyBoard.halfHeight);

        int index = Random.Range(0, 7);
        Color32 color = Color.white;

        switch (index)
        {
            // I : ÇÏ´Ã»ö
            case 0:
                color = new Color32(115, 251, 253, 255);
                GameObject go1 = Tile.CreateTile(tetrominoNode, new Vector2(-2f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                break;

            // J : ÆÄ¶õ»ö
            case 1:
                color = new Color32(0, 33, 245, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color, pooling: true);
                break;

            // L : ±Ö»ö
            case 2:
                color = new Color32(243, 168, 59, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1.0f), color, pooling: true);
                break;

            // O : ³ë¶õ»ö
            case 3:
                color = new Color32(255, 253, 84, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1f), color, pooling: true);
                break;

            // S : ³ì»ö
            case 4:
                color = new Color32(117, 250, 76, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;

            // T : ÀÚÁÖ»ö
            case 5:
                color = new Color32(155, 47, 246, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                break;

            // Z : »¡°£»ö
            case 6:
                color = new Color32(235, 51, 35, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;
        }

        return MyTetromino;
    }

    public Tetromino EnemySpawn()
    {
        GameObject go = FindById((int)EObjectType.EnemyTetromino);
        Transform tetrominoNode = go.transform;

        Vector2Int enemyBoardPos = Managers.EnemyBoard.enemyBoardPos;

        tetrominoNode.transform.rotation = Quaternion.identity;
        tetrominoNode.transform.position = enemyBoardPos + new Vector2(0, Managers.EnemyBoard.halfHeight);

        int index = Random.Range(0, 7);
        Color32 color = Color.white;

        switch (index)
        {
            // I : ÇÏ´Ã»ö
            case 0:
                color = new Color32(115, 251, 253, 255);
                GameObject go1 = Tile.CreateTile(tetrominoNode, new Vector2(-2f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                break;

            // J : ÆÄ¶õ»ö
            case 1:
                color = new Color32(0, 33, 245, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color, pooling: true);
                break;

            // L : ±Ö»ö
            case 2:
                color = new Color32(243, 168, 59, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1.0f), color, pooling: true);
                break;

            // O : ³ë¶õ»ö
            case 3:
                color = new Color32(255, 253, 84, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 1f), color, pooling: true);
                break;

            // S : ³ì»ö
            case 4:
                color = new Color32(117, 250, 76, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, -1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;

            // T : ÀÚÁÖ»ö
            case 5:
                color = new Color32(155, 47, 246, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                break;

            // Z : »¡°£»ö
            case 6:
                color = new Color32(235, 51, 35, 255);
                Tile.CreateTile(tetrominoNode, new Vector2(-1f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 1f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(0f, 0f), color, pooling: true);
                Tile.CreateTile(tetrominoNode, new Vector2(1f, 0f), color, pooling: true);
                break;
        }

        return EnemyTetromino;
    }

}