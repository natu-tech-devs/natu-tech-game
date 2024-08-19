using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField]
    public Attack attack = new();

    public float attackRatio = 1;

    public float attackSpeed = 1f;
    public float attackTime = 0f;



    public void Attack(Defender defender)
    {
        attackTime = Time.time;
        if (defender.onAttack != null)
            defender.onAttack(this.attack * attackRatio);
    }

    public IEnumerator asyncAttack(Defender defender){
        if(attackTime <= 1 / attackSpeed){
            yield return new WaitForSeconds((1/ attackSpeed)  -  attackTime);
            Attack(defender);
        }
        yield return new WaitForFixedUpdate();
    }
}
