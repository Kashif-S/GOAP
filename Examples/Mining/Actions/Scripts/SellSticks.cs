﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SellSticks", menuName = "ScriptableObjects/Actions/SellSticks")]
public class SellSticks : ExampleAction
{
    public override int GetCost()
    {
        return 1;
    }

    public override InventoryState UpdateState(InventoryState state)
    {
        state.sticks -= 1;
        state.money += GameManager.stickValue;
        return state;
    }

    public override bool ValidateState(InventoryState state)
    {
        return state.sticks >= 1;
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
        agent.sticks -= 1;
        agent.money += GameManager.stickValue;
        return true;
    }

    protected override bool validateLocation(MiningAgent agent, InventoryState state)
    {
        return Vector3.Distance(agent.transform.position, GameManager.storeLocation) <= GameManager.distanceTolerance;
    }
}
