using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : abState
{
    public MovingState(StateLists stateLists, int stateId)
        : base(stateLists, stateId)
    {
    }

    public override void OnEnter()
    {
        StateComponents stateComponents = GetStateComponents();
        Tetromino tetromino = (Tetromino)stateComponents.GetParentProcess();

        Vector3 moveDir = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDir.x = -1;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDir.x = 1;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            moveDir.y = -1;

        Managers.Board.MoveTo(tetromino, moveDir, false);

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
