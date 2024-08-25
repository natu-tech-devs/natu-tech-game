using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Defender))]
[RequireComponent(typeof(StatusEffect))]
[RequireComponent(typeof(Attacker))]
public class BarrelHazard : MonoBehaviour
{
    public Health health;
    public Defender defender;
    public StatusEffect statusEffec;
    public Attacker attacker;
    private Player player => GameManager.player;
    public bool wickLit = false;
    public int countDown = 3;
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
        
        StartCoroutine(explosionAndDie());
    }


    private bool validation()
    {
        Debug.Log("BarrelHazard validation");
        return this != null;
    }

    private async void onAttack(Attack attack)
    {
        if (attack.element == ElemetType.FIRE && !wickLit)
        {
            Debug.Log("Acendi");
            wickLit = true;
            
        }

        if(attack.element == ElemetType.WATER  && wickLit){
            wickLit = false;
            Debug.Log("Apaguei");
        }

        health.takeDamage(0);
    }

    private IEnumerator turnAction()
    {

        if (wickLit){
           if(wickLit){
            Debug.Log("<---------->");
            Debug.Log("EXPLODI POHA");
            Debug.Log("<---------->");
            attacker.Attack(player.defender);
        }
        
        yield return new WaitForSeconds(1);
        Destroy(this);
        yield return null;
        }

        collider.enabled = false;
        yield return null;
    }

    private IEnumerator explosionAndDie(){

        if(wickLit){
            Debug.Log("<---------->");
            Debug.Log("EXPLODI POHA");
            Debug.Log("<---------->");
            attacker.Attack(player.defender);
        }
        
        yield return new WaitForSeconds(1);
        Destroy(this);
        yield return null;
    }
}
