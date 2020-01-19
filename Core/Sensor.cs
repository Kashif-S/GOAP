using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensor<T, S> : ScriptableObject where T : GoapAgent where S : State
{
    public abstract void UpdateState(T agent, S state);
}
