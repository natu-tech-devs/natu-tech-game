using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turn
{

    //TODO add priority queue
    private List<TurnAction> actions = new();



    public void startTurn(Action onEnd = null)
    {
        actions.ForEach((action) => action.takeAction());
        if (onEnd != null)
        {
            onEnd();
        }
    }


    public void addTurnAction(TurnAction action)
    {
        actions.Add(action);
    }

}
