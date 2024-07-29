using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private bool canAttack = true;


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

    public void testAction()
    {
    }

    void turnPrep()
    {
        Debug.Log("player turn prep");
    }

    public void playerInputAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            sphereAttack();
        }
    }

    public void sphereAttack()
    {
        if (!canAttack) return;
        Sphere instance = Instantiate(sphere).GetComponent<Sphere>();
        if (instance)
        {
            canAttack = false;
            instance.transform.position = transform.position + transform.forward * 2;
            instance.launch(transform.forward * launchForce, attacker, () =>
            {
                canAttack = true;
                GameManager.gm.endPlayerTurn();
            },() => {
                GameManager.gm.playerSuccess();
            });
        }
    }

    void Start()
    {
        GameManager.gm.turnPrep += turnPrep;

        health = GetComponent<Health>();
        attacker = GetComponent<Attacker>();
        defender = GetComponent<Defender>();
        statusEffect = GetComponent<StatusEffect>();

        defender.onAttack += (Attack attack) =>
        {
            Debug.Log("Player defense");
            health.takeDamage(attack.damage);
        };

        health.onDamage += () =>
        {
            Debug.Log("Player Health: " + health.health);
        };
    }

}
