using System.Collections;
using System.Collections.Generic;

public class Planner<T, S> where T : GoapAgent where S : State
{
    // TODO: actions should be a set (?)
    public Plan<T, S> GeneratePlan(Goal<S> goal, S state, List<Action<T, S>> actions)
    {
        // TODO: Sort actions based on cost

        // TODO: Replace this with a heap
        List<PlannerState> openSet = new List<PlannerState>();
        HashSet<State> closedSet = new HashSet<State>();
        openSet.Add(new PlannerState(state, null, null, 0));

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
                return new Plan<T, S>(goal, RetraceActions(currentState));
            }

            for (int i = 0; i < actions.Count; i++)
            {
                S newState = actions[i].UpdateState(currentState.state);
                if (closedSet.Contains(newState) && actions[i].ValidateState(currentState.state))
                {
                    int newGCost = currentState.gCost + actions[i].GetCost();
                    // TODO: calculate hCost
                    PlannerState newPlannerState = new PlannerState(newState, currentState, actions[i], newGCost);
                    if (!openSet.Contains(newPlannerState))
                    {
                        openSet.Add(newPlannerState);
                    }
                }
            }
        }
        return null;
    }

    private List<Action<T, S>> RetraceActions(PlannerState plannerState)
    {
        List<Action<T, S>> actionSequence = new List<Action<T, S>>();

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
        public Action<T, S> action { get; private set; }

        public int gCost { get; private set; }
        public int hCost { get; private set; }

        public PlannerState(S state, PlannerState previousState, Action<T, S> action, int gCost)
        {
            this.state = state;
            this.previousState = previousState;
            this.action = action;
            this.gCost = gCost;
            // TODO: Replace this with an actual heuristic
            this.hCost = 0;
        }

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }
    }
}
