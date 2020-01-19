using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plan<T, S, A> where T : GoapAgent where S : State where A : Action<T, S>
{
    public Goal<S> goal { get; private set; }
    private Queue<A> actionQueue;

    public Plan(Goal<S> goal, List<A> actionSequence)
    {
        this.goal = goal;
        this.actionQueue = new Queue<A>(actionSequence);
    }

    public A getNextAction()
    {
        if(actionQueue.Count != 0)
        {
            return actionQueue.Dequeue();
        } else {
            return null;
        }
    }

    public int GetLength()
    {
        return actionQueue.Count;
    }
}
