using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager 
{
    #region 
    private Queue<Turn> turns = new();


#nullable enable
    public Turn? currentTurn => turns.Count > 0 ? turns.Peek() : (Turn?)null;
#nullable disable


    #endregion

    public void startCurrentTurn()
    {
        Debug.Log("Start Current Turn");
        currentTurn?.startTurn(() =>
        {
            turns.Enqueue(new Turn());
        });
    }

    public void addCurrentTurnAction(TurnAction action)
    {
        if(currentTurn == null) turns.Enqueue(new());
        currentTurn?.addTurnAction(action);
    }

}
