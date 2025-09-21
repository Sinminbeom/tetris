using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const int MAX_LOBBY_ROOM_COUNT = 10;
    public enum EScene
    {
        Unknown,
        TitleScene,
        GameScene,
        SingleGameScene,
        MultiGameScene
    }
    public enum ETouchEvent
    {
        PointerUp,
        PointerDown,
        Click,
        Pressed,
        BeginDrag,
        Drag,
        EndDrag,
    }

    // protobuf·Î »¬ °Í
    public enum EObjectType
    {
        SingleTetromino,
        MyTetromino,
        EnemyTetromino
    }
}
