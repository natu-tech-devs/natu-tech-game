
using System;
using UnityEngine;

[Serializable]
public struct Attack {
    [SerializeField]
    public float damage;
    [SerializeField]
    public AttackType type;
    [SerializeField]
    public ElemetType element;
}