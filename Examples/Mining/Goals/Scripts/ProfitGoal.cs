using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProfitGoal", menuName = "ScriptableObjects/Goals/ProfitGoal")]
public class ProfitGoal : ExampleGoal
{
    public override bool ValidateState(InventoryState state)
    {
        return state.money >= 10000;
    }
}
