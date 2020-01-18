using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensor<S> : ScriptableObject where S : State
{
    public abstract void UpdateState(S state);
}
