using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum E_TETROMINO_STATE
{
    Idle,
    Falling,
    DownMoving,
    LeftMoving,
    RightMoving,
    Rotating,
    Dropping,
    Locked,
    GameOver
}