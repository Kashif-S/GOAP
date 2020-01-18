using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfitGoal : ExampleGoal
{
    public override bool ValidateState(InventoryState state)
    {
        return state.money >= 10000;
    }
}
