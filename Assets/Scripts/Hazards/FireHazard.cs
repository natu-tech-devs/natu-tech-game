using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Defender))]
[RequireComponent(typeof(StatusEffect))]
[RequireComponent(typeof(Attacker))]
public class FireHazard : MonoBehaviour
{
    public Health health;
    public Defender defender;
    public StatusEffect statusEffec;
    public Attacker attacker;

    private Player player => GameManager.player;

    public int countDown = 10;

    [SerializeField]
    private GameObject fireParticles;
    [SerializeField]
    private GameObject smokeParticles;
    [SerializeField]
    private GameObject strongFire;

    private new Collider collider;

    void Start()
    {
        health = GetComponent<Health>();
        defender = GetComponent<Defender>();
        statusEffec = GetComponent<StatusEffect>();
        attacker = GetComponent<Attacker>();
        collider = GetComponent<Collider>();

        health.die = die;
        defender.onAttack += onAttack;

        GameManager.gm.turnManager.addTurnActionHelper(turnAction, validation, this, countDown);
    }

    void die(){
        collider.enabled = false;
        StartCoroutine(smokeAndDie());
    }


    private bool validation()
    {
        Debug.Log("FireHazard validation");
        return this != null;
    }

    private void onAttack(Attack attack)
    {
        if (attack.element == ElemetType.WATER)
        {
            Debug.Log("Tomei");
            health.takeDamage(attack.damage);
        }
    }

    private IEnumerator turnAction()
    {
        Debug.Log("FireHazard Action");

        attacker.Attack(player.defender);

        fireParticles.SetActive(false);
        strongFire.SetActive(true);
        collider.enabled = false;
        yield return null;
    }

    private IEnumerator smokeAndDie(){
        fireParticles.SetActive(false);
        smokeParticles.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(this);
        yield return null;
    }
}
