using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action<T, S> : ScriptableObject where T : GoapAgent where S : State
{
    public abstract int GetCost();
    public abstract bool ValidateState(S state);
    public abstract S UpdateState(S state);
    public bool act(T agent, S state)
    {
        if(validateLocation(agent, state))
        {
            return performAction(agent, state);
        } else {
            moveToLocation(agent, state);
            return false;
        }
    }
    protected abstract bool validateLocation(T agent, S state);
    protected abstract void moveToLocation(T agent, S state);
    protected abstract bool performAction(T agent, S state);
}
