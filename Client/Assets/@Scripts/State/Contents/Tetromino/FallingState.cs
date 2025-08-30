using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingState : abState
{
    public FallingState(StateLists stateLists, int stateId)
        : base(stateLists, stateId)
    {
    }

    public override void OnEnter()
    {

    }

    public override void OnLeave()
    {
        
    }

    public override void OnProcEveryFrame()
    {
        StateComponents stateComponents = GetStateComponents();
        Tetromino tetromino = (Tetromino)stateComponents.GetParentProcess();

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Moving);

        else if (Input.GetKeyDown(KeyCode.UpArrow))
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Rotating);

        else if (Input.GetKeyDown(KeyCode.Space))
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Dropping);

        if (Time.time > Managers.Board.nextFallTime)
        {
            Managers.Board.nextFallTime = Time.time + Managers.Board.fallCycle;
            if (!Managers.Board.MoveTo(tetromino, Vector3Int.down, false))
            {
                stateComponents.ChangeState((int)E_TETROMINO_STATE.Locked);
            }
        }
    }

    public override void OnProcOnce()
    {
        
    }
}
