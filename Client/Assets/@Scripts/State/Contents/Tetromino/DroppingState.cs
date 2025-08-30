using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingState : abState
{
    public DroppingState(StateLists stateLists, int stateId)
        : base(stateLists, stateId)
    {
    }

    public override void OnEnter()
    {
        StateComponents stateComponents = GetStateComponents();
        Tetromino tetromino = (Tetromino)stateComponents.GetParentProcess();

        while (Managers.Board.MoveTo(tetromino, Vector3.down, false)) { }
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
        stateComponents.ChangeState((int)E_TETROMINO_STATE.Locked);
    }
}
