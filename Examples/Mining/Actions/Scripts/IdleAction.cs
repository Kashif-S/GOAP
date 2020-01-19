using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleAction", menuName = "ScriptableObjects/Actions/IdleAction")]
public class IdleAction : ExampleAction
{
    public override int GetCost()
    {
        return 0;
    }

    public override InventoryState UpdateState(InventoryState state)
    {
        return state;
    }

    public override bool ValidateState(InventoryState state)
    {
        return true;
    }

    protected override int GetTotalProgess()
    {
        return 1;
    }

    protected override void moveToLocation(MiningAgent agent, InventoryState state)
    {
        return;
    }

    protected override bool performAction(MiningAgent agent, InventoryState state)
    {
        return false;
    }

    protected override bool validateLocation(MiningAgent agent, InventoryState state)
    {
        return true;
    }
}
