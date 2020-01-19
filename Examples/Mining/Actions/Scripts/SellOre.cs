using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SellOre", menuName = "ScriptableObjects/Actions/SellOre")]
public class SellOre : ExampleAction
{
    public override int GetCost()
    {
        return 1;
    }

    public override InventoryState UpdateState(InventoryState state)
    {
        state.ore -= 1;
        state.money += GameManager.oreValue;
        return state;
    }

    public override bool ValidateState(InventoryState state)
    {
        return state.ore >= 1;
    }

    protected override int GetTotalProgess()
    {
        return 1;
    }

    protected override void moveToLocation(MiningAgent agent, InventoryState state)
    {
        agent.navAgent.SetDestination(GameManager.storeLocation);
    }

    protected override bool performAction(MiningAgent agent, InventoryState state)
    {
        agent.ore -= 1;
        agent.money += GameManager.oreValue;
        return true;
    }

    protected override bool validateLocation(MiningAgent agent, InventoryState state)
    {
        return Vector3.Distance(agent.transform.position, GameManager.storeLocation) <= GameManager.distanceTolerance;
    }
}
