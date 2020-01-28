using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner<T, S, A> where T : GoapAgent where S : State where A : Action<T, S>
{
    // TODO: actions should be a set (?)
    public Plan<T, S, A> GeneratePlan(Goal<S> goal, S state, List<A> actions)
    {
        // TODO: Sort actions based on cost

        // TODO: Replace this with a heap
        List<PlannerState> openSet = new List<PlannerState>();
        HashSet<S> closedSet = new HashSet<S>();
        openSet.Add(new PlannerState(state, null, null, 0, goal.CalculateHCost(state)));

        while (openSet.Count > 0)
        {
            PlannerState currentState = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentState.fCost || (openSet[i].fCost == currentState.fCost && openSet[i].hCost < currentState.hCost))
                {
                    currentState = openSet[i];
                }
            }

            openSet.Remove(currentState);
            closedSet.Add(currentState.state);

            if (goal.ValidateState(currentState.state))
            {
                return new Plan<T, S, A>(goal, RetraceActions(currentState));
            }

            for (int i = 0; i < actions.Count; i++)
            {
                S newState = actions[i].UpdateState(currentState.state.DeepClone());
                if (!closedSet.Contains(newState) && actions[i].ValidateState(currentState.state))
                {
                    int newGCost = currentState.gCost + actions[i].GetCost();
                    int hCost = goal.CalculateHCost(newState);
                    PlannerState newPlannerState = new PlannerState(newState, currentState, actions[i], newGCost, hCost);
                    if (!openSet.Contains(newPlannerState))
                    {
                        Debug.Log(newState.GetHashCode());
                        openSet.Add(newPlannerState);
                    }
                }
            }
        }
        return null;
    }

    private List<A> RetraceActions(PlannerState plannerState)
    {
        List<A> actionSequence = new List<A>();

        while (plannerState.previousState != null)
        {
            actionSequence.Add(plannerState.action);
            plannerState = plannerState.previousState;
        }
        actionSequence.Reverse();
        return actionSequence;
    }

    private class PlannerState
    {
        public S state { get; private set; }
        public PlannerState previousState { get; private set; }
        public A action { get; private set; }

        public int gCost { get; private set; }
        public int hCost { get; private set; }

        public PlannerState(S state, PlannerState previousState, A action, int gCost, int hCost)
        {
            this.state = state;
            this.previousState = previousState;
            this.action = action;
            this.gCost = gCost;
            this.hCost = hCost;
        }

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            PlannerState p = (PlannerState)obj;
            return state.Equals(p.state);
        }

        public override int GetHashCode()
        {
            return state.GetHashCode();
        }
    }
}
