using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Sphere : MonoBehaviour
{

    public Action onHit;

    public Attacker attacker;

    public new Rigidbody rigidbody => GetComponent<Rigidbody>();

    public Action onDestroyAction;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        if (gameObject)
            GameObject.Destroy(gameObject);
    }

    public void launch(Vector3 force, Attacker attacker, Action callback = null, Action onHit = null)
    {
        this.attacker = attacker;
        rigidbody.AddForce(force, ForceMode.VelocityChange);
        if (callback != null)
        {
            onDestroyAction += callback;
            this.onHit += onHit;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Defender defender = collision.gameObject.GetComponent<Defender>();
        if (defender != null && attacker != null)
        {
            if (onHit != null)
                onHit();
            attacker.Attack(defender);
        }

        if (defender != null)
            GameObject.Destroy(gameObject);
    }

    public void OnDestroy()
    {
        if (onDestroyAction != null) onDestroyAction();
    }

}
