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


    public IEnumerator startTurn(Action onEnd = null)
    {
        Debug.Log("Start Turn");
        for(int i = 0; i < actions.Count; i++){
            yield return actions[i].takeAction();
        }
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
