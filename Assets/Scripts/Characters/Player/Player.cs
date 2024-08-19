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
    [HideInInspector]
    public Health health;
    [HideInInspector]
    public Attacker attacker;
    [HideInInspector]
    public Defender defender;

    [HideInInspector]
    public StatusEffect statusEffect;

    public GameObject sphere;

    public bool attacked = false;

    private Func<bool> canAttack = () => GameManager.gm.isPlayerTurn;


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
        attacked = false;
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
        if (!canAttack() || attacked) return;
        Sphere instance = Instantiate(sphere).GetComponent<Sphere>();
        if (instance)
        {
            attacked = true;
            instance.transform.position = transform.position + transform.forward * 2;
            instance.launch(transform.forward * launchForce, attacker, () =>
            {
                GameManager.gm.endPlayerTurn();
            }, () =>
            {
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
