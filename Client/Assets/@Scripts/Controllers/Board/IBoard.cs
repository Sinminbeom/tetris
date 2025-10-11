using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IBoard
{
    int halfWidth { get; }
    int halfHeight { get; }
    float NextFallTime { get; set; }
    float FallCycle { get; }
    public GameObject Root { get; set; }
    Vector2Int Pos { get; set; }

    IBackground Background { get; set; }
    Tetromino Tetromino { get; set; }

    void Init();
    void Spawn();
    void Spawn(ETetrominoType tetrominoType);
    bool MoveTo(Vector3 pos, bool isRotate);
    void SyncMove(Vector3 pos, bool isRotate);
    bool CanMove();
    void AddObject();
    void SyncAddObject();
    void CheckCompleteRow();
}
