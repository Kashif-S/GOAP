using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapController<T, S, A, G, E> : MonoBehaviour where T : GoapAgent where S : State where A : Action<T, S> where G : Goal<S> where E : Sensor<S>
{
    public List<A> actions;
    public List<E> sensors;
    public List<G> goals;
    public A idleAction;
    public T agent;
    public S state;

    [HideInInspector] private Planner<T, S, A> planner;
    [HideInInspector] private Plan<T, S, A> plan;
    [HideInInspector] private A currentAction;
    [HideInInspector] protected G currentGoal;

    void Awake()
    {
        InitializeState();
        planner = new Planner<T, S, A>();
    }

    private void FixedUpdate()
    {
        UpdateState();
        UpdateGoal();
        ExecutePlan();
    }

    private void UpdateState()
    {
        foreach (E sensor in sensors)
        {
            sensor.UpdateState(state);
        }
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

    protected abstract void InitializeState();
    protected abstract void UpdateGoal();
}
