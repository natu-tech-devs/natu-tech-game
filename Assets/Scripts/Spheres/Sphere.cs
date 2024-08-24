using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Sphere))]
public class Sphere : MonoBehaviour
{

    public Action onHit;

    public Attacker attacker;

    private Attacker launchAttack;

    public new Rigidbody rigidbody => GetComponent<Rigidbody>();

    public Action onDestroyAction;

    [SerializeField]
    private GameObject allySphereEffect;
    [SerializeField]
    private GameObject enemySphereEffect;


    IEnumerator Start()
    {
        attacker = GetComponent<Attacker>();
        yield return new WaitForSeconds(4);
        if (gameObject)
            GameObject.Destroy(gameObject);
    }

    public void launch(Vector3 force, Attacker attacker, Action callback = null, Action onHit = null)
    {
        rigidbody.AddForce(force, ForceMode.VelocityChange);
        if (callback != null)
        {
            onDestroyAction += callback;
            this.onHit += onHit;
        }
    }

    private void handleAllyEffect(Attacker attacker, Defender defender)
    {
        if (allySphereEffect != null)
            Instantiate(allySphereEffect,defender.transform.position,Quaternion.identity);
    }

    private void handleEnemyEffect(Attacker attacker, Defender defender)
    {
        attacker.Attack(defender);
        if (enemySphereEffect != null)
            Instantiate(enemySphereEffect,defender.transform.position,Quaternion.identity);
    }

    public void hit(Defender defender)
    {

        if (defender != null)
        {
            
            var localAttacker = launchAttack != null ? launchAttack : attacker;
            if (onHit != null)
                onHit();

            if (localAttacker != null)
            {
                if (localAttacker.agentSide != defender.agentSide)
                {
                    handleEnemyEffect(localAttacker, defender);
                }
                else
                {
                    handleAllyEffect(localAttacker, defender);
                }
            }
        }
        if (defender != null)
            GameObject.Destroy(gameObject);

    }

    public void OnCollisionEnter(Collision collision)
    {
        Defender defender = collision.gameObject.GetComponent<Defender>();
        if (defender != null) hit(defender);
    }

    public void OnTriggerEnter(Collider collider)
    {
        Defender defender = collider.gameObject.GetComponent<Defender>();
        if (defender != null) hit(defender);
    }

    public void OnDestroy()
    {
        if (onDestroyAction != null) onDestroyAction();
    }

}
