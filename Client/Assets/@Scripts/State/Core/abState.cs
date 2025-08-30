using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abState
{
    protected StateLists stateLists;
    protected int stateId;
    protected bool IsRunProcOnce;
    protected object parentsProcess;

    public abState(StateLists stateLists, int stateId)
    {
        this.stateLists = stateLists;
        this.stateId = stateId;
        this.IsRunProcOnce = false;
    }

    public StateManager GetStateManager()
    {
        if (stateLists == null)
            return null;

        return stateLists.GetStateManager();
    }

    public StateComponents GetStateComponents()
    {
        var mgr = GetStateManager();
        if (mgr != null)
            return mgr.GetParentsStateComponents();

        return null;
    }

    public object GetParentsProcess()
    {
        var mgr = GetStateManager();
        if (mgr == null)
            return null;

        var stateComponents = mgr.GetParentsStateComponents();
        if (stateComponents == null)
            return null;

        return stateComponents.GetParentProcess();
    }

    protected void SetParentProcess()
    {
        parentsProcess = GetParentsProcess();
    }

    public void BaseOnEnter(object stateParamDto = null)
    {
        IsRunProcOnce = false;
        SetParentProcess();
        OnEnter();
    }

    public abstract void OnEnter();
    public abstract void OnLeave();
    public abstract void OnProcOnce();
    public abstract void OnProcEveryFrame();

    public void BaseOnProcEveryFrame()
    {
        if (!IsRunProcOnce)
        {
            OnProcOnce();
            IsRunProcOnce = true;
        }

        OnProcEveryFrame();
    }
}
