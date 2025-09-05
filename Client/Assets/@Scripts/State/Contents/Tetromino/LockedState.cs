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

        //if (Managers.SingleBoard.CanMove(tetromino))
        //{
        //    Managers.SingleBoard.AddObject(tetromino.transform);
        //    Managers.SingleObject.Spawn();
        //    Managers.SingleBoard.CheckCompleteRow();
        //}

        if (Managers.MyBoard.CanMove(tetromino))
        {
            Managers.MyBoard.AddObject(tetromino.transform);
            Managers.MultiObject.Spawn();
            Managers.MyBoard.CheckCompleteRow();
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

        //if (Managers.SingleBoard.CanMove(tetromino))
        if (Managers.MyBoard.CanMove(tetromino))
        {
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Falling);
        }
        else
        {
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Idle);
        }
    }
}
