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
            { (int)E_TETROMINO_STATE.Falling, new FallingState(this, (int)E_TETROMINO_STATE.Falling) },
            { (int)E_TETROMINO_STATE.Moving, new MovingState(this, (int)E_TETROMINO_STATE.Moving) },
            { (int)E_TETROMINO_STATE.Rotating, new RotatingState(this, (int)E_TETROMINO_STATE.Rotating) },
            { (int)E_TETROMINO_STATE.Dropping, new DroppingState(this, (int)E_TETROMINO_STATE.Dropping) },
            { (int)E_TETROMINO_STATE.Locked, new LockedState(this, (int)E_TETROMINO_STATE.Locked) },
        };
    }
}
