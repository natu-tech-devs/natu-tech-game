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
    private bool canAttack = false;
    private List<Defender> burning = new();
    private void Start()
    {
        attacker = GetComponent<Attacker>();
        GameManager.gm.turnPrep += prep;

        GameManager.gm.turnManager.addTurnAction(new TurnAction()
        {
            action = die,
            validation = () => this != null
        }, turnSpan);
    }

    private void prep()
    {
        canAttack = true;
        burn();
    }

    private IEnumerator die()
    {
        Destroy(gameObject);
        yield return null;
    }

    public void procBurn(Defender defender)
    {
        burning.Add(defender);
    }

    private void burn()
    {
        foreach (var def in burning)
        {
            if (def != null)
                attacker.Attack(def);
        }
    }
    private void handleCollision(Defender defender)
    {
        Debug.Log("Quase");
        if (defender != null && defender.agentSide != attacker.agentSide)
        {
            burning.Add(defender);
            canAttack = false;
        }

    }

    public void OnTriggerEnter(Collider collider)
    {
        if (!canAttack) return;

        Defender defender = collider.gameObject.GetComponent<Defender>();
        if (defender != null)
            handleCollision(defender);
    }




    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("alou, can attack" + canAttack);
        if (!canAttack) return;

        Defender defender = collision.gameObject.GetComponent<Defender>();
        if (defender != null)
            handleCollision(defender);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!canAttack) return;
        Defender defender = collision.gameObject.GetComponent<Defender>();
        if (defender != null)
            handleCollision(defender);
    }


    private void OnDestroy()
    {
        GameManager.gm.turnPrep -= prep;
    }
}
