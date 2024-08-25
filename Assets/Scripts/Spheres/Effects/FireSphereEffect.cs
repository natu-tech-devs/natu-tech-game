using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class FireSphereEffect : MonoBehaviour
{
    [SerializeField]
    private int turnSpan = 3;
    [SerializeField]
    private Attacker attacker;
    private void Start()
    {
        attacker = GetComponent<Attacker>();
    }


    private IEnumerator die()
    {
        Destroy(gameObject);
        yield return null;
    }

    public void destroyMe()
    {
        Destroy(gameObject);
    }


    private void handleCollision(Defender defender)
    {
        if (defender != null && defender.agentSide != attacker.agentSide)
        {
            var defenderStatus = defender.GetComponent<StatusEffect>();
            if (defenderStatus != null)
            {
                defenderStatus.applyStatus(new()
                {
                    elemetType = ElemetType.FIRE,
                    damage = attacker.attack.damage,
                    duration = turnSpan
                });
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Defender defender = collision.gameObject.GetComponent<Defender>();
        if (defender != null)
            handleCollision(defender);
    }
}
