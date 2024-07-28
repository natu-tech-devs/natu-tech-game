using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Sphere : MonoBehaviour
{

    public UnityEvent onHit;

    public Attacker attacker;

    public new Rigidbody rigidbody => GetComponent<Rigidbody>();

    public Action onDestroyAction;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        if (gameObject)
            GameObject.Destroy(gameObject);
    }

    public void launch(Vector3 force, Attacker attacker, Action callback = null)
    {
        this.attacker = attacker;
        rigidbody.AddForce(force);
        if (callback != null)
            onDestroyAction += callback;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Defender defender = collision.gameObject.GetComponent<Defender>();
        if (defender != null && attacker != null)
            attacker.Attack(defender);
        if (defender != null)
            GameObject.Destroy(gameObject);
    }

    public void OnDestroy()
    {
        if (onDestroyAction != null) onDestroyAction();
        Debug.Log("Destroy sphere");
    }

}
