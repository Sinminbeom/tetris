using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLists
{
    protected Dictionary<int, abState> stateList;
    protected StateManager stateManager;

    public StateLists(Dictionary<int, abState> stateList)
    {
        _setStateList(stateList);
    }

    protected void _setStateList(Dictionary<int, abState> stateList)
    {
        this.stateList = stateList;
    }

    public abState GetState(int stateId)
    {
        return stateList[stateId];
    }

    public void SetStateManager(StateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public StateManager GetStateManager()
    {
        return stateManager;
    }
}