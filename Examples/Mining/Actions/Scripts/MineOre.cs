using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MineOre", menuName = "ScriptableObjects/Actions/MineOre")]
public class MineOre : ExampleAction
{
    private const int totalProgess = 300;

    public override int GetCost()
    {
        return (totalProgess * 100) / GameManager.oreValue;
    }

    public override InventoryState UpdateState(InventoryState state)
    {
        state.ore += 1;
        return state;
    }

    public override bool ValidateState(InventoryState state)
    {
        return state.hasPickaxe;
    }

    protected override int GetTotalProgess()
    {
        return totalProgess;
    }

    protected override void moveToLocation(MiningAgent agent, InventoryState state)
    {
        agent.navAgent.SetDestination(GameManager.rocksLocation);
    }

    protected override bool performAction(MiningAgent agent, InventoryState state)
    {
        if (agent.actionProgress >= GetTotalProgess())
        {
            agent.ore += 1;
            return true;
        }
        agent.actionProgress++;
        return false;
    }

    protected override bool validateLocation(MiningAgent agent, InventoryState state)
    {
        return Vector3.Distance(agent.transform.position, GameManager.rocksLocation) <= GameManager.distanceTolerance;
    }
}
