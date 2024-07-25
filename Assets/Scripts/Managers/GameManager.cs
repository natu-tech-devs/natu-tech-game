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
    public Action playerAction;

    private void Awake()
    {
        if (GameManager.gm == null)
        {
            GameManager.gm = this;
        }
    }

    public void endPlayerTurn()
    {
        if (playerAction != null)
            playerAction();
        startTurn();
    }

    private void startTurn()
    {

        if (turnPrep != null)
            turnPrep();
        turnManager.startCurrentTurn();
    }
}
