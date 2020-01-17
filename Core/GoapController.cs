using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapController<T, S> : MonoBehaviour where T : GoapAgent where S : State 
{
    public List<Action<T, S>> actions;
    public List<Sensor<S>> sensors;
    public List<Goal<S>> goals;
    public Action<T, S> idleAction;
    public T agent;
    public S state;

    [HideInInspector] private Planner<T, S> planner;
    [HideInInspector] private Plan<T, S> plan;
    [HideInInspector] private Action<T, S> currentAction;
    [HideInInspector] private Goal<S> currentGoal;

    void Awake()
    {
        planner = new Planner<T, S>();
    }

    private void FixedUpdate()
    {
        UpdateState();
        UpdateGoal();
        ExecutePlan();
    }

    private void UpdateState()
    {
        foreach (Sensor<S> sensor in sensors)
        {
            sensor.UpdateState(state);
        }
    }

    private void UpdateGoal()
    {
        currentGoal = goals[0];
    }

    private void ExecutePlan()
    {
        plan = plan != null ? plan : planner.GeneratePlan(currentGoal, state, actions);
        if (plan == null)
        {
            idleAction.act(agent, state);
            return;
        }

        currentAction = currentAction != null ? currentAction : plan.getNextAction();
        if(currentAction == null || currentAction.ValidateState(state) || plan.goal != currentGoal)
        {
            plan = null;
            return;
        }

        if(currentAction.act(agent, state))
        {
            currentAction = null;
        }
    }
}
