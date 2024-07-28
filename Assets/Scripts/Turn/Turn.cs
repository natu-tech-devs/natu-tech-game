using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turn
{

    //TODO add priority queue
    private List<TurnAction> actions = new();

    public bool playerSuccess = false;


    public void startTurn(Action onEnd = null)
    {
        Debug.Log("Start Turn");
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
