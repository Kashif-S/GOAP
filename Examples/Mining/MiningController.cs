using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningController : GoapController<MiningAgent, InventoryState, ExampleAction, ExampleGoal, ExampleSensor>
{
    protected override void InitializeState()
    {
        this.state = new InventoryState();
    }

    protected override void UpdateGoal()
    {
        this.currentGoal = goals[0];
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 1000, 200), "Action Progress: " + agent.actionProgress);
        GUI.Label(new Rect(20, 40, 1000, 200), "Remaining Steps: " + (plan != null ? plan.GetLength() : 0));
        GUI.Label(new Rect(20, 60, 1000, 200), "Money: " + state.money);
        GUI.Label(new Rect(20, 80, 1000, 200), "Sticks: " + state.sticks);
        GUI.Label(new Rect(20, 100, 1000, 200), "Logs: " + state.logs);
        GUI.Label(new Rect(20, 120, 1000, 200), "Ore: " + state.ore);
        GUI.Label(new Rect(20, 140, 1000, 200), "Has Axe: " + state.hasAxe);
        GUI.Label(new Rect(20, 160, 1000, 200), "Has Pickaxe: " + state.hasPickaxe);
    }
}
