using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField]
    public Attack attack = new();

    public float attackRatio = 1;



    public void Attack(Defender defender)
    {
        if (defender.onAttack != null)
            defender.onAttack(this.attack * attackRatio);
    }
}
