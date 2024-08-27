using System.Collections;
using System.ComponentModel.Design;
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
    public float wobble = 0.005f;
    private float randomWoble = 0;

    public ElemetType currentElement = ElemetType.NEUTRAL;

    private void Start()
    {
        defender = GetComponent<Defender>();
        health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody>();

        defender.onAttack += onAttack;

        randomWoble = Random.value;

        if (transformOrbit != null)
        {
            StartCoroutine(orbitRoutine(transformOrbit, orbitSpeed));
        }
    }

    public IEnumerator orbitRoutine(Transform target, float force = 1f)
    {
        yield return new WaitForFixedUpdate();
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.position.x, 0, target.position.z));
        while (target != null)
        {
            orbitAround(target, force * Time.deltaTime, distance, transform.position.y);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    private void orbitAround(Transform target, float force = 1f, float distance = 1f, float height = 0)
    {

        transform.position = target.position + new Vector3((float)System.Math.Sin(Time.time + randomWoble * 10), 0, (float)System.Math.Cos(Time.time + randomWoble * 10)) * distance;
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
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
        var force = wobble * (float)System.Math.Cos(randomWoble + Time.time * 3) * Time.deltaTime;
        transform.position = transform.position + transform.up * force / 5;
    }

}
