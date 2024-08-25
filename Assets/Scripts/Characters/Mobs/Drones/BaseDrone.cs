using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Defender))]
[RequireComponent(typeof(Rigidbody))]
public class BaseDrone : MonoBehaviour
{
    private Defender defender;
    private Health health;
    private Rigidbody _rigidbody;

    public Transform transformOrbit;
    public float orbitSpeed = 1f;
    public float wobble = 10f;

    public ElemetType currentElement = ElemetType.NEUTRAL;

    private void Start()
    {
        defender = GetComponent<Defender>();
        health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody>();

        defender.onAttack += onAttack;

        if (transformOrbit != null)
        {
            StartCoroutine(orbitRoutine(transformOrbit, orbitSpeed));
        }
    }

    private IEnumerator orbitRoutine(Transform target, float force = 1f)
    {
        while (target != null)
        {
            orbitAround(target, force * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    private void orbitAround(Transform target, float force = 1f)
    {
        Vector3 pointVector = target.position - transform.position;
        Vector3 movementVector = Vector3.Cross(target.up, pointVector).normalized;
        _rigidbody.AddForce(movementVector * force * 100);
    }

    private void onAttack(Attack attack, AgentSide agentSide)
    {
        if (defender.agentSide != agentSide && attack.element != currentElement)
        {
            health.takeDamage(attack.damage);
        }
    }

    private void FixedUpdate()
    {
        var force = wobble * (float)Math.Cos(Time.time) * transform.up * Time.deltaTime;
        _rigidbody.AddForce(force);
    }

}
