using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal<S> : ScriptableObject where S : State
{
    public abstract bool ValidateState(S state);
}
