using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void onEffect(ElemetType currentType, ElemetType newType);
public class StatusEffect : MonoBehaviour
{

    public List<onEffect> onEffects = new();

    [SerializeField]
    public ElemetType currentElement { get; private set; } = ElemetType.WATER;

    public void appyEffecnt(ElemetType elemetType)
    {
        onEffects.ForEach((effect) => effect(currentElement, elemetType));
        currentElement = elemetType;
    }
}
