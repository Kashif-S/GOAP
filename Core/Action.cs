using System.Collections;
using System.Collections.Generic;

public interface IAction
{
    int GetCost();
    bool ValidateState(State state);
    void UpdateState(State state);
}
