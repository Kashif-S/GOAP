using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickSticks", menuName = "ScriptableObjects/Actions/PickSticks")]
public class PickSticks : ExampleAction
{
    private const int totalProgess = 100;

    public override int GetCost()
    {
        return 1000;
    }

    public override InventoryState UpdateState(InventoryState state)
    {
        state.sticks += 1;
        return state;
    }

    public override bool ValidateState(InventoryState state)
    {
        return true;
    }

    protected override int GetTotalProgess()
    {
        return totalProgess;
    }

    protected override void moveToLocation(MiningAgent agent, InventoryState state)
    {
        agent.navAgent.SetDestination(GameManager.woodsLocation);
    }

    protected override bool performAction(MiningAgent agent, InventoryState state)
    {
        if(agent.actionProgress >= GetTotalProgess())
        {
            agent.sticks += 1;
            return true;
        }
        agent.actionProgress++;
        return false;
    }

    protected override bool validateLocation(MiningAgent agent, InventoryState state)
    {
        return Vector3.Distance(agent.transform.position, GameManager.woodsLocation) <= GameManager.distanceTolerance;
    }
}
