using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProfitGoal", menuName = "ScriptableObjects/Goals/ProfitGoal")]
public class ProfitGoal : ExampleGoal
{
    public override int CalculateHCost(InventoryState state)
    {
        return 10000 - state.money;
    }

    public override bool ValidateState(InventoryState state)
    {
        return state.money >= 10000;
    }
}
