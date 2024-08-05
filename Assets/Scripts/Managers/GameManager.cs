using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TurnManager turnManager = new();

    public static Player player;

    public static GameManager gm;


    public Action turnPrep;
    public Action playerFailure;
    public Action playerAction;

    public bool getPlayerSuccess => turnManager.getPlayerSuccess;

    private void Awake()
    {
        if (GameManager.gm == null)
        {
            GameManager.gm = this;
        }
    }

    public void endPlayerTurn()
    {
        if (!turnManager.getPlayerSuccess && playerFailure != null)
        {
            playerFailure();
        }
        var coroutine = StartCoroutine(startTurn());
    }

    public void playerSuccess()
    {
        Debug.Log("Player Success!!!!");
        turnManager.playerSuccess();
    }

    private IEnumerator startTurn(Action endTurn = null)
    {

        if (turnPrep != null)
            turnPrep();
        yield return turnManager.startCurrentTurn();

        if(endTurn != null) endTurn();
    }

    public void Lose(){
        Debug.Log("You lose");
    }
}
