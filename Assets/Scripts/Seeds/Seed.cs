using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Defender))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider))]
public class Seed : MonoBehaviour
{
    private Defender defender;
    private Health health;
    [SerializeField]
    private float playerDamage = 40;
    void Start()
    {
        defender = GetComponent<Defender>();
        health = GetComponent<Health>();

        defender.onAttack += onAttack;

        GameManager.gm.turnPrep += prep;

        health.die += die;
    }

    private void prep(){
        GameManager.seeds.Add(gameObject);
    }

    private void die()
    {
        GameManager.player.health.takeDamage(playerDamage);
        GameManager.gm.turnPrep -= prep;
        Destroy(gameObject);
    }

    private void onAttack(Attack attack)
    {
        health.takeDamage(attack.damage);
    }
}
