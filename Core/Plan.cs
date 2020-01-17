using System.Collections;
using System.Collections.Generic;

public class Plan<T, S> where T : GoapAgent where S : State
{
    public Goal<S> goal { get; private set; }
    private Queue<Action<T, S>> actionQueue;

    public Plan(Goal<S> goal, List<Action<T, S>> actionSequence)
    {
        this.goal = goal;
        this.actionQueue = new Queue<Action<T, S>>(actionSequence);
    }

    public Action<T, S> getNextAction()
    {
        if(actionQueue.Count != 0)
        {
            return actionQueue.Dequeue();
        } else {
            return null;
        }
    }
}
