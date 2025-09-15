using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMovingState : abState
{
    public LeftMovingState(StateLists stateLists, int stateId)
        : base(stateLists, stateId)
    {
    }

    public override void OnEnter()
    {
        StateComponents stateComponents = GetStateComponents();
        Tetromino tetromino = (Tetromino)stateComponents.GetParentProcess();

        IBoard board = tetromino.Board;
        board.MoveTo(Vector3.left, false);
    }

    public override void OnLeave()
    {

    }

    public override void OnProcEveryFrame()
    {
        StateComponents stateComponents = GetStateComponents();
        Tetromino tetromino = (Tetromino)stateComponents.GetParentProcess();
        stateComponents.ChangeState((int)E_TETROMINO_STATE.Falling);
    }

    public override void OnProcOnce()
    {

    }
}
