using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExampleAction : Action<MiningAgent, InventoryState>
{
    protected abstract int GetTotalProgess();
}
