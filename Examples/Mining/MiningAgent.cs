using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiningAgent : GoapAgent
{

    public NavMeshAgent navAgent;
    public int sticks = 0;
    public int logs = 0;
    public int ore = 0;
    public int money = 0;
    public bool hasAxe = false;
    public bool hasPickaxe = false;

    public void MoveToLocation(Vector3 destination)
    {
        navAgent.SetDestination(destination);
    }
}
