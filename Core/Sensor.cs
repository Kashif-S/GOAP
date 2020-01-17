using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensor<T> : ScriptableObject where T : State
{
    public abstract void UpdateState(T state);
}
