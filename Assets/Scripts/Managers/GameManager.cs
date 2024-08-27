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

    public static List<GameObject> seeds = new();


    public Action turnPrep;
    public Action playerFailure;
    public Action playerAction;

    public bool getPlayerSuccess => turnManager.getPlayerSuccess;

    public bool isPlayerTurn = true;

    private void Awake()
    {
        if (GameManager.gm == null)
        {
            GameManager.gm = this;
        }
    }

    public GameObject nearSeed(Vector3 pos)
    {
        float dist = float.MaxValue;
        GameObject currentNearSeed = null;

        foreach (var seed in seeds)
        {
            if(seed == null) continue;
            float localDist = Vector3.Distance(pos, seed.transform.position);
            if (localDist < dist)
            {
                dist = localDist;
                currentNearSeed = seed;
            }
        }
        return currentNearSeed;
    }

    public void endPlayerTurn()
    {
        isPlayerTurn = false;
        if (!turnManager.getPlayerSuccess && playerFailure != null)
        {
            playerFailure();
        }
        StartCoroutine(startTurn());
    }

    public void playerSuccess()
    {
        turnManager.playerSuccess();
    }

    private IEnumerator startTurn(Action endTurn = null)
    {
        if (turnPrep != null)
            turnPrep();
        yield return turnManager.startCurrentTurn();

        if (endTurn != null) endTurn();
        seeds.Clear();
        isPlayerTurn = true;
    }

    public void Lose()
    {
        Debug.Log("You lose");
    }
}
