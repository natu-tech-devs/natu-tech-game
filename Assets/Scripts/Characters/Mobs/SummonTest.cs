using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Defender))]
public class SummonTest : MonoBehaviour
{
    uint attackCooldown = 0;
    uint attackTimer = 0;
    Attacker attacker;
    Defender defender;
    Health health;

    [SerializeField]
    private GameObject sphere;

    void Start()
    {
        health = GetComponent<Health>();
        attacker = GetComponent<Attacker>();
        defender = GetComponent<Defender>();

        GameManager.gm.turnPrep += turnPrep;
    }

    public void turnPrep()
    {
        Debug.Log("Summon prep");
        attackTimer++;
        GameManager.gm.turnManager.addCurrentTurnAction(new TurnAction()
        {
            validation = () =>
            {
                bool result = attackTimer >= attackCooldown; 
                Debug.Log("summon validation: " + result);
                return result;
            },
            action = turnAction
        });
    }


    public IEnumerator turnAction()
    {
        Debug.Log("Summon action");
        attackTimer = 0;
        var instance = Instantiate(sphere).GetComponent<Sphere>();

        if (instance != null)
        {
            Vector3 playerPos = GameManager.player.transform.position;
            Vector3 playerDirection = (playerPos - transform.position).normalized;
            instance.transform.position = transform.position + playerDirection * 2;
            Vector3 launchForce = ProjectileLauncher.CalculateLaunchForce(instance.transform.position, playerPos, 60f);
            instance.launch(launchForce  , attacker);
        }
        yield return null;
    }
}
