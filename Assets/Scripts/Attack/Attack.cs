using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct Attack
{
    [SerializeField]
    public float damage;
    [SerializeField]
    public AttackType type;
    [SerializeField]
    public ElemetType element;
    [SerializeField]

    public static Attack operator *(Attack attack, int attackMultiplier)
    {
        attack.damage *= attackMultiplier;
        return attack;
    }

    public static Attack operator *(Attack attack, float attackMultiplier)
    {
        attack.damage *= attackMultiplier;
        return attack;
    }
}