using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public Health health;
    void Awake()
    {
        if (GameManager.player == null)
        {
            GameManager.player = this;
        }

        health = GetComponent<Health>();
    }

    public void testAction(){
        GameManager.gm.endPlayerTurn();
    }

    void turnPrep(){
        Debug.Log("player turn prep");
    }

    void Start()
    {
        GameManager.gm.turnPrep += turnPrep;
    }
}
