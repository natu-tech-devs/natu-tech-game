using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Defender))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Collider))]
public class Truck : MonoBehaviour
{
    private Health health;
    private Defender defender;
    private Attacker attacker;


    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private int startTurn = 3;


    private AgentState state = AgentState.IDLE;

    private Vector3 originalPosition = new();



    private void Start()
    {
        health = GetComponent<Health>();
        defender = GetComponent<Defender>();
        attacker = GetComponent<Attacker>();



        defender.onAttack += (Attack attack) => health.takeDamage(attack.damage);

        GameManager.gm.turnPrep += prep;

        originalPosition = transform.position;

    }

    private void prep()
    {
        Debug.Log("Truck prep: " + state);
        if (state == AgentState.IDLE)
        {
            state = AgentState.ATTACKING;
             GameManager.gm.turnManager.addTurnAction(new TurnAction() {
                action = attackNearSeed,
                validation = () => state == AgentState.ATTACKING
             },startTurn);
        }

    }


    private IEnumerator moveTowardsObj(Transform _transform)
    {
        float timer = 0;

        float distance = Vector3.Distance(_transform.position, transform.position);

        while (timer <= 10f && state == AgentState.ATTACKING && distance >= 0.1f && _transform != null)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _transform.position, speed * Time.deltaTime);
            distance = Vector3.Distance(transform.position,_transform.position);
            transform.LookAt(_transform);
            yield return new WaitForFixedUpdate();
        }
        yield return returnToOrigin();
    }

    private IEnumerator returnToOrigin()
    {

        float distance = Vector3.Distance(originalPosition, transform.position);

        while (distance >= 0.1f)
        {
            yield return new WaitForFixedUpdate();
            transform.position = Vector3.MoveTowards(transform.position, originalPosition,3 * speed * Time.deltaTime);
            distance = Vector3.Distance(originalPosition,transform.position);
            transform.LookAt(originalPosition);
        }

        yield return null;
    }


private IEnumerator attackNearSeed(){
    GameObject seed = GameManager.gm.nearSeed(transform.position);
    if(seed == null) yield break;
    Transform seedTransform = seed.transform;
    Debug.Log("Truck attacking at posion: " + seedTransform.position);
    yield return begginAttack(seedTransform);
    Debug.Log("End of Truck action");
}

    private IEnumerator begginAttack(Transform _transform)
    {
        yield return moveTowardsObj(_transform);
        if(state == AgentState.ATTACKING) state = AgentState.IDLE;
    }



    private void OnDestroy()
    {
        GameManager.gm.turnPrep -= prep;
    }


    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Truck collision");
        Defender defender = collision.gameObject.GetComponent<Defender>();
        if (defender != null)
        {
            attacker.Attack(defender);
        }

    }

    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Truck collision");
        Defender defender = collider.gameObject.GetComponent<Defender>();
        if (defender != null)
        {
            attacker.Attack(defender);
        }

    }
}