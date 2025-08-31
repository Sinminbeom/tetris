using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedState : abState
{

    public LockedState(StateLists stateLists, int stateId)
        : base(stateLists, stateId)
    {
    }

    public override void OnEnter()
    {
        StateComponents stateComponents = GetStateComponents();
        Tetromino tetromino = (Tetromino)stateComponents.GetParentProcess();

        if (Managers.Board.CanMove(tetromino))
        {
            Managers.Board.AddObject(tetromino.transform);
            Managers.Object.Spawn();
            Managers.Board.CheckCompleteRow();
        }
    }

    public override void OnLeave()
    {
    }

    public override void OnProcEveryFrame()
    {
        
    }

    public override void OnProcOnce()
    {
        StateComponents stateComponents = GetStateComponents();
        Tetromino tetromino = (Tetromino)stateComponents.GetParentProcess();

        if (Managers.Board.CanMove(tetromino))
        {
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Falling);
        }
        else
        {
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Idle);
        }
    }
}
