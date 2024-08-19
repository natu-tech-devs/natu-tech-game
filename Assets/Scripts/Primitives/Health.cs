using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public delegate bool healthDelegate(float health, float value);

public class Health : MonoBehaviour
{


    [SerializeField]
    public float health = 100f;

    public Action onDamage;

    public UnityEvent onDeath;
    public Action onDeathEvent;

    public List<healthDelegate> deathConditions = new();
    public List<healthDelegate> damageCondigitons = new();

    public List<healthDelegate> healConditions = new();

    public Action die;


    public void takeDamage(float damage)
    {
        if (!damageCondigitons.Aggregate(true, (acc, current) => acc && current(health, damage))) return;

        health -= damage;
        if (onDamage != null)
            onDamage();
        if (health <= 0)
        {
            health = 0;
            if (die != null)
                die();
        }
    }

    public void heal(float value)
    {
        if (!healConditions.Aggregate(true, (acc, current) => acc && current(health, value))) return;
        health += value;
    }

    public void _die()
    {
        //prevent death,  List.Aggregate its like javascript Array.reduce
        if (!deathConditions.Aggregate(true, (acc, current) => acc && current(health, 0))) return;

        if (onDamage != null)
            onDamage();
        GameObject.Destroy(gameObject);
    }
}
