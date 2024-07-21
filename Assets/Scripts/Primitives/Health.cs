using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public delegate bool healthDelegate(float health,float value);

public class Health : MonoBehaviour
{


    [SerializeField]
    public float health { get; private set; } = 100f;

    public UnityEvent onDamage;

    public UnityEvent onDeath;

    public List<healthDelegate> deathConditions = new();
    public List<healthDelegate> damageCondigitons = new();

    public List<healthDelegate> healConditions = new();


    public void takeDamage(float damage)
    {
        if (!damageCondigitons.Aggregate(true, (acc, current) => acc && current(health,damage))) return;

        health -= damage;
        if (health <= 0)
        {
            health = 0;
            onDamage.Invoke();
            die();
        }
    }

    public void heal(float value)
    {
        if (!healConditions.Aggregate(true, (acc, current) => acc && current(health,value))) return;
        health += value;
    }

    public void die()
    {
        //prevent death,  List.Aggregate its like javascript Array.reduce
        if (!deathConditions.Aggregate(true, (acc, current) => acc && current(health,0))) return;

        onDamage.Invoke();
        GameObject.Destroy(this);
    }
}
