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

        IBoard board = tetromino.Board;
        if (board.CanMove())
        {
            board.AddObject();
            board.Spawn();
            board.CheckCompleteRow();
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

        if (tetromino.Board.CanMove())
        {
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Falling);
        }
        else
        {
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Idle);
        }
    }
}
