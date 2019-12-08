using System.Collections;
using System.Collections.Generic;

public interface IAction
{
    int GetCost();
    bool ValidateState(State state);
    State UpdateState(State state);
}
