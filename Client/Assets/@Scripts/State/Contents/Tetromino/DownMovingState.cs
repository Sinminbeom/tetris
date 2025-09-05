using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMovingState : abState
{
    public DownMovingState(StateLists stateLists, int stateId)
        : base(stateLists, stateId)
    {
    }

    public override void OnEnter()
    {
        StateComponents stateComponents = GetStateComponents();
        Tetromino tetromino = (Tetromino)stateComponents.GetParentProcess();

        //Managers.SingleBoard.MoveTo(tetromino, Vector3.down, false);
        Managers.MyBoard.MoveTo(tetromino, Vector3.down, false);

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
