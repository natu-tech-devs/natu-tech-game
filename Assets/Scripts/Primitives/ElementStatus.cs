using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ElementStatus : IEquatable<ElementStatus>
{
    public ElemetType elemetType;
    public float damage;
    public int duration;

    public ElementStatus()
    {
    }


    public override int GetHashCode()
    {
        return elemetType.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is ElementStatus)
            return Equals(obj as ElementStatus);
        return false;
    }
    public bool Equals(ElementStatus other)
    {
        return other != null && elemetType == other.elemetType;
    }

    public static bool operator >=(ElementStatus a, ElementStatus b)
    {
        if (a.elemetType != b.elemetType) return a.elemetType >= b.elemetType;
        if (a.damage == 0 && b.damage == 0) return a.duration >= b.duration;
        return a.damage * a.duration >= b.duration * b.damage;
    }


    public static bool operator <=(ElementStatus a, ElementStatus b)
    {
        if (a.elemetType != b.elemetType) return a.elemetType <= b.elemetType;
        if (a.damage == 0 && b.damage == 0) return a.duration <= b.duration;
        return a.damage * a.duration <= b.duration * b.damage;
    }
}
