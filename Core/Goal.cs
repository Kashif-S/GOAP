using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal<T> : ScriptableObject where T : State
{
    public abstract bool ValidateState(T state);
}
