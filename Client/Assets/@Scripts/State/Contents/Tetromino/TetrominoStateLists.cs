using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoStateLists : StateLists
{
    public TetrominoStateLists()
        : base(new Dictionary<int, abState>())
    {
        stateList = new Dictionary<int, abState>
        {
            { (int)E_TETROMINO_STATE.Idle, new IdleState(this, (int)E_TETROMINO_STATE.Idle) },
            { (int)E_TETROMINO_STATE.Falling, new FallingState(this, (int)E_TETROMINO_STATE.Falling) },
            { (int)E_TETROMINO_STATE.DownMoving, new DownMovingState(this, (int)E_TETROMINO_STATE.DownMoving) },
            { (int)E_TETROMINO_STATE.LeftMoving, new LeftMovingState(this, (int)E_TETROMINO_STATE.LeftMoving) },
            { (int)E_TETROMINO_STATE.RightMoving, new RightMovingState(this, (int)E_TETROMINO_STATE.RightMoving) },
            { (int)E_TETROMINO_STATE.Rotating, new RotatingState(this, (int)E_TETROMINO_STATE.Rotating) },
            { (int)E_TETROMINO_STATE.Dropping, new DroppingState(this, (int)E_TETROMINO_STATE.Dropping) },
            { (int)E_TETROMINO_STATE.Locked, new LockedState(this, (int)E_TETROMINO_STATE.Locked) },
        };
    }
}
