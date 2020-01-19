using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class State
{
    public abstract override bool Equals(object obj);
    public abstract override int GetHashCode();
}
