using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningController : GoapController<MiningAgent, InventoryState, ExampleAction, ExampleGoal, ExampleSensor>
{
    protected override void InitializeState()
    {
        this.state = new InventoryState();
    }

    protected override void UpdateGoal()
    {
        this.currentGoal = goals[0];
    }
}
