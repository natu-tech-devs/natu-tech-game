using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void onEffect(ElemetType currentType, ElemetType newType);
public class StatusEffect : MonoBehaviour
{


    public onEffect onEffects;

    public float defenseMultiplier = 1;
    public float attackMultiplier = 1;


    [SerializeField]
    public ElemetType currentElement = ElemetType.NEUTRAL;

    public void applyEffect(ElemetType elemetType)
    {
        onEffects(currentElement, elemetType);
    }
}
