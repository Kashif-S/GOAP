using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryState : State
{
    public int money = 0;
    public int sticks = 0;
    public int logs = 0;
    public int ore = 0;
    public bool hasAxe = false;
    public bool hasPickaxe = false;

    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        InventoryState s = (InventoryState)obj;
        return (money == s.money) && (sticks == s.sticks) && (logs == s.logs) && (hasAxe == s.hasAxe);
    }
    
    public override int GetHashCode()
    {
        return money * 2 + sticks * 3 + logs * 5 + ore * 7 + (hasAxe ? 11 : 0) + (hasPickaxe ? 13 : 0);
    }
}
