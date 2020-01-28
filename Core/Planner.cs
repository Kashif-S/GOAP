using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner<T, S, A> where T : GoapAgent where S : State where A : Action<T, S>
{
    // TODO: actions should be a set (?)
    public Plan<T, S, A> GeneratePlan(Goal<S> goal, S state, List<A> actions)
    {
        // TODO: Sort actions based on cost

        PlannerStateHeap openSet = new PlannerStateHeap();
        HashSet<S> closedSet = new HashSet<S>();
        openSet.Add(new PlannerState(state, null, null, 0, goal.CalculateHCost(state)));

        while (openSet.Count > 0)
        {
            PlannerState currentState = openSet.Pop();
            if(closedSet.Contains(currentState.state))
            {
                continue;
            }
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
                    openSet.Add(newPlannerState);
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

    private class PlannerStateHeap
    {
        private List<PlannerState> Heap = new List<PlannerState>();

        public int Count
        {
            get
            {
                return Heap.Count;
            }
        }

        private static int GetLeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }
        private static int GetRightChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 2;
        }
        private static int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private bool HasLeftChild(int index)
        {
            return GetLeftChildIndex(index) < Heap.Count;
        }
        private bool HasRightChild(int index)
        {
            return GetRightChildIndex(index) < Heap.Count;
        }
        private bool HasParent(int index)
        {
            return GetParentIndex(index) >= 0;
        }

        private PlannerState LeftChild(int index)
        {
            return Heap[GetLeftChildIndex(index)];
        }
        private PlannerState RightChild(int index)
        {
            return Heap[GetRightChildIndex(index)];
        }
        private PlannerState Parent(int index)
        {
            return Heap[GetParentIndex(index)];
        }

        private void Swap(int indexOne, int indexTwo)
        {
            PlannerState temp = Heap[indexOne];
            Heap[indexOne] = Heap[indexTwo];
            Heap[indexTwo] = temp;
        }

        public PlannerState Pop()
        {
            if (Heap.Count == 0)
            {
                return null;
            }

            PlannerState state = Heap[0];
            Heap[0] = Heap[Heap.Count - 1];
            Heap.RemoveAt(Heap.Count - 1);

            HeapifyDown();
            return state;
        }

        public void Add(PlannerState state)
        {
            Heap.Add(state);
            HeapifyUp();
        }

        private void HeapifyDown()
        {
            int index = 0;
            while(HasLeftChild(index))
            {
                int smallerChildIndex = GetLeftChildIndex(index);
                if(HasRightChild(index) && CompareGreaterThan(LeftChild(index), RightChild(index)))
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if(CompareGreaterThan(Heap[smallerChildIndex], Heap[index])) {
                    break;
                } else {
                    Swap(index, smallerChildIndex);
                }
                index = smallerChildIndex;

            }
        }

        private void HeapifyUp()
        {
            int index = Heap.Count - 1;
            while (HasParent(index) && CompareGreaterThan(Parent(index), Heap[index]))
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }

        private bool CompareGreaterThan(PlannerState left, PlannerState right)
        {
            if(left.fCost == right.fCost)
            {
                return left.hCost > right.hCost;
            }
            return left.fCost > right.fCost;
        }
    }
}
