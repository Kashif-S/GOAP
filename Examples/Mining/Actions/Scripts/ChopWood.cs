using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChopWood", menuName = "ScriptableObjects/Actions/ChopWood")]
public class ChopWood : ExampleAction
{
    private const int totalProgess = 200;

    public override int GetCost()
    {
        return (totalProgess * 100) / GameManager.logValue;
    }

    public override InventoryState UpdateState(InventoryState state)
    {
        state.logs += 1;
        return state;
    }

    public override bool ValidateState(InventoryState state)
    {
        return state.hasAxe;
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
        if (agent.actionProgress >= GetTotalProgess())
        {
            agent.logs += 1;
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
