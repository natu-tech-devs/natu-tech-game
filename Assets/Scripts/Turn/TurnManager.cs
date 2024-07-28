using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager 
{
    #region 
    private List<Turn> turns = new();
    private int turnIndex = 0;


#nullable enable
    public Turn? currentTurn => turns.Count > 0 ? turns[turnIndex] : (Turn?)null;
#nullable disable


    #endregion

    public void startCurrentTurn()
    {
        Debug.Log("Start Current Turn");
        currentTurn?.startTurn(() =>
        {
            turnIndex++;
        });
    }

    public void addCurrentTurnAction(TurnAction action)
    {
        if(currentTurn == null) {
            turns[turnIndex] = new Turn();
        }
        currentTurn?.addTurnAction(action);
    }

    private void fillTurns(int fillsTo){
        if(fillsTo < 0) return;

        int finalSize = turns.Count - turnIndex + fillsTo;
        for(int counter = turns.Count - turnIndex; counter < finalSize; counter++){
            turns.Add(new Turn());
        }

    }

    public void addTurnAction(TurnAction action, int skipTurns = 0){
        fillTurns(skipTurns);
        turns[turnIndex + skipTurns - 1].addTurnAction(action);
    }

}
