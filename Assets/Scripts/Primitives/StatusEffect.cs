using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void onEffect(ElemetType currentType, ElemetType newType);
public delegate void onStatus(ElementStatus status);

public class StatusEffect : MonoBehaviour
{


    public onEffect onEffects;
    public onStatus onStatus;

    public float defenseMultiplier = 1;
    public float attackMultiplier = 1;

    public HashSet<ElementStatus> elementStatuses = new();

    public Action handleStatusesAction;


    [SerializeField]
    public ElemetType currentElement = ElemetType.NEUTRAL;

    private Health health;

    public void init(Health health)
    {
        this.health = health;
    }

    public void statusUpdate()
    {
        handleStatuses();
    }

    public void applyEffect(ElemetType elemetType)
    {
        if (onEffects != null)
            onEffects(currentElement, elemetType);
    }

    public void defaultApplyStatus(ElementStatus status)
    {
        if (
            elementStatuses.TryGetValue(status, out ElementStatus foundStatus)
        )
        {
            if (status >= foundStatus)
            {
                elementStatuses.Remove(foundStatus);
                elementStatuses.Add(status);
            }
        }
        else
            elementStatuses.Add(status);

    }

    public void applyStatus(ElementStatus status)
    {
        if (onStatus != null)
        {
            onStatus(status);
        }
        else defaultApplyStatus(status);
    }

    public void defaultHandleStatuses()
    {
        List<ElementStatus> toRemove = new();
        foreach (var status in elementStatuses)
        {
            if (status.duration <= 0)
            {
                toRemove.Add(status);
            }
            else
            {
                health.takeDamage(status.damage);
                status.duration--;
            }
        }
        foreach (var status in toRemove)
        {
            elementStatuses.Remove(status);
        }
    }

    public void handleStatuses()
    {
        if (handleStatusesAction != null)
        {
            handleStatusesAction();
        }
        else defaultHandleStatuses();
    }

}
