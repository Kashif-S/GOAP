using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySensor", menuName = "ScriptableObjects/Sensors/InventorySensor")]
public class InventorySensor : ExampleSensor
{
    public override void UpdateState(MiningAgent agent, InventoryState state)
    {
        state.money = agent.money;
        state.logs = agent.logs;
        state.sticks = agent.sticks;
        state.ore = agent.ore;
        state.hasAxe = agent.hasAxe;
        state.hasPickaxe = agent.hasPickaxe;
    }
}
