using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(StatusEffect))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Defender))]
public class Player : MonoBehaviour
{
    public Health health;
    public Attacker attacker;
    public Defender defender;

    public StatusEffect statusEffect;

    public GameObject sphere;


    [SerializeField]
    private float launchForce = 1f;
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

    public void sphereAttack(){
        Sphere instance = Instantiate(sphere,transform).GetComponent<Sphere>();
        if(instance){
            instance.launch(transform.forward * launchForce,attacker);
        }

        GameManager.gm.endPlayerTurn();
    }

    void Start()
    {
        GameManager.gm.turnPrep += turnPrep;

        health = GetComponent<Health>();
        attacker = GetComponent<Attacker>();
        defender = GetComponent<Defender>();
        statusEffect = GetComponent<StatusEffect>();
    }

}
