using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningController : GoapController<MiningAgent, InventoryState>
{
    protected override void InitializeState()
    {
        this.state = new InventoryState();
    }
}
