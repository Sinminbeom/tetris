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

        Managers.Board.AddObject(tetromino.transform);
        Managers.Object.Spawn();
        Managers.Board.CheckCompleteRow();
    }

    public override void OnLeave()
    {
        StateComponents stateComponents = GetStateComponents();
        Tetromino tetromino = (Tetromino)stateComponents.GetParentProcess();

        if (!Managers.Board.CanMove(tetromino))
            Managers.UI.ShowPopupUI<UI_GamePopup>();
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
