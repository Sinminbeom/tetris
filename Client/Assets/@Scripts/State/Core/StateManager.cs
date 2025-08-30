using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    private StateLists stateInfoLists;
    private StateComponents parentsStateComponents;
    private int? currentState;

    public StateManager(StateLists stateInfoLists, StateComponents parentsStateComponents)
    {
        this.parentsStateComponents = parentsStateComponents;
        _setStateInfo(stateInfoLists);
    }

    private void _setStateInfo(StateLists stateLists)
    {
        stateLists.SetStateManager(this);
        this.stateInfoLists = stateLists;
    }

    public StateComponents GetParentsStateComponents()
    {
        return parentsStateComponents;
    }

    public abState GetState(int stateId)
    {
        return stateInfoLists.GetState(stateId);
    }

    public abState ChangeState(int stateId, object stateParamDto = null)
    {

        if (currentState.HasValue)
        {
            GetState(currentState.Value).OnLeave();
        }

        currentState = stateId;
        GetState(stateId).BaseOnEnter(stateParamDto);

        return stateInfoLists.GetState(currentState.Value);
    }

    public abState GetCurrentState()
    {
        if (currentState == null)
            return null;

        return stateInfoLists.GetState(currentState.Value);
    }

    public int? GetCurrentStateId()
    {
        return currentState;
    }
}