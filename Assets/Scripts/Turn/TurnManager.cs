using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager
{
    #region 
    private List<Turn> turns = new() { new Turn() };
    private int turnIndex = 0;


#nullable enable
    public Turn? currentTurn => turns.Count > turnIndex ? turns[turnIndex] : (Turn?)null;
#nullable disable

    public bool getPlayerSuccess => currentTurn != null ? currentTurn.playerSuccess : false;

    #endregion

    public void playerSuccess()
    {
        if (currentTurn == null)
            turns.Add(new Turn());
        currentTurn.playerSuccess = true;
    }


    public IEnumerator startCurrentTurn()
    {
        if (currentTurn == null)
        {
            fillTurns(turnIndex);
        }
        yield return currentTurn?.startTurn(() =>
        {
            if (turns.Count < turnIndex++)
            {
                turns.Add(new Turn());
            }
        });
    }

    public void addCurrentTurnAction(TurnAction action)
    {
        if (currentTurn == null)
        {
            turns.Add(new Turn());
        }
        currentTurn?.addTurnAction(action);
    }

    private void fillTurns(int fillsTo)
    {
        if (fillsTo < 0) return;

        int finalSize = turns.Count - turnIndex + fillsTo;
        for (int counter = turns.Count - turnIndex; counter < finalSize; counter++)
        {
            turns.Add(new Turn());
        }

    }

    public TurnAction addTurnAction(TurnAction action, int skipTurns = 0)
    {
        fillTurns(skipTurns);
        turns[turnIndex + skipTurns].addTurnAction(action);
        return action;
    }

    public void addTurnActionHelper(Func<IEnumerator> action, Func<bool> validator, object reference,int turnsAhead){
        addTurnAction(new TurnAction(){
            action = action,
            validation = validator,
            cleanUp = () => reference == null
        },turnsAhead);
    }

}
