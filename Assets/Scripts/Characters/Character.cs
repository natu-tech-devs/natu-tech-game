using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class Character  : MonoBehaviour,ICharacter
{
    [SerializeField]
    private float health = 100f;

    [SerializeField]
    public CharStates state {get;private set;} = CharStates.IDLE;
    [SerializeField]
    public CharSide side {get;private set;} = CharSide.NEUTRAL;
    public ElemetType elemetType = ElemetType.WATER;

    public Character @target;



    public UnityEvent onDeath,onDamage,onAttack;

    public float getHealth()
    {
        return health;
    }

    public void die()
    {
        onDeath.Invoke();
        GameObject.Destroy(this);
    }

    public void getAttacked(Attack attack)
    {
        onDamage.Invoke();
        health -= attack.damage;
        if(health <= 0){
            die();
            return;
        }
    }

    public virtual void attack(Attack attack)
    {
        onAttack.Invoke();
    }

    public CharStates getState()
    {
        return state;
    }

    public CharSide getSide()
    {
        return side;
    }

    public ElemetType GetElemetType()
    {
        return elemetType;
    }
}
