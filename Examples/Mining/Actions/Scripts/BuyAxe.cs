using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuyAxe", menuName = "ScriptableObjects/Actions/BuyAxe")]
public class BuyAxe : ExampleAction
{
    public override int GetCost()
    {
        return 1;
    }

    public override InventoryState UpdateState(InventoryState state)
    {
        state.hasAxe = true;
        return state;
    }

    public override bool ValidateState(InventoryState state)
    {
        return state.money >= GameManager.axeValue;
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
        agent.money -= GameManager.axeValue;
        agent.hasAxe = true;
        return true;
    }

    protected override bool validateLocation(MiningAgent agent, InventoryState state)
    {
        return Vector3.Distance(agent.transform.position, GameManager.storeLocation) <= GameManager.distanceTolerance;
    }
}
