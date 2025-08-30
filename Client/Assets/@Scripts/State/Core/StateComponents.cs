using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateComponents
{
    public StateManager stateManager;
    protected object parentProcess;

    private int? reserveState;

    public StateComponents(object parentProcess, StateLists stateInfoLists, int? initState)
    {
        this.stateManager = new StateManager(stateInfoLists, this);
        this.parentProcess = parentProcess;

        if (initState.HasValue)
        {
            ChangeState(initState.Value);
        }
    }

    public object GetParentProcess()
    {
        return parentProcess;
    }

    public void ChangeState(int stateId)
    {
        this.reserveState = stateId;
    }

    public void OnChangeState()
    {
        if (reserveState.HasValue)
        {
            stateManager.ChangeState(reserveState.Value);
            reserveState = null;
        }
    }

    public void OnProcOnce()
    {
        var currentState = stateManager.GetCurrentState();
        if (currentState != null)
        {
            currentState.OnProcOnce();
        }
    }

    public void OnProcEveryFrame()
    {
        var currentState = stateManager.GetCurrentState();
        if (currentState != null)
            currentState.BaseOnProcEveryFrame();
    }
}