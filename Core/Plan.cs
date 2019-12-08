using System.Collections;
using System.Collections.Generic;

public class Plan
{
    private List<IAction> actionSequence;

    public Plan(List<IAction> actionSequence)
    {
        this.actionSequence = actionSequence;
    }
}
