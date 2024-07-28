using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(StatusEffect))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Defender))]
public class MobTest : MonoBehaviour
{

    private float defenseDebufRatio = 0.75f;

    private Health health;
    private StatusEffect statusEffect;

    private Attacker attacker;
    private Defender defender;


    private int debufDuration = 2;
    private int debufCurrentDuration = 0;

    private Dictionary<ElemetType, ElemetType> weakness = new(){
        { ElemetType.WATER,ElemetType.EARTH},
        { ElemetType.EARTH,ElemetType.AIR },
        { ElemetType.FIRE,ElemetType.WATER },
        { ElemetType.AIR,ElemetType.FIRE },

    };
    void turnPrep()
    {
        if (debufCurrentDuration >= debufDuration)
        {
            debufCurrentDuration = 0;
            defender.defenseRatio /= defenseDebufRatio;
        }

        GameManager.gm.turnManager.addCurrentTurnAction(new TurnAction(){
            action = turnAction
        });
    }



    void turnAction()
    {
        if(!GameManager.gm.getPlayerSuccess){
            attacker.Attack(GameManager.player.defender);
        }
    }
    void Start()
    {
        GameManager.gm.turnPrep += turnPrep;
        health = GetComponent<Health>();
        statusEffect = GetComponent<StatusEffect>();
        attacker = GetComponent<Attacker>();
        defender = GetComponent<Defender>();
        statusEffect.onEffects += onEffect;


        defender.onAttack += (Attack attack) =>
        {
            statusEffect.applyEffect(attack.element);
            health.takeDamage(attack.damage);
            Debug.Log(health.health);
        };



    }

    void onEffect(ElemetType currentType, ElemetType newType)
    {
        Debug.Log("Mob on effect");
        if (this.weakness[currentType] == newType)
        {
            if (debufCurrentDuration == 0)
            {
                this.defender.defenseRatio *= defenseDebufRatio;
                this.debufCurrentDuration = 1;
            }

        }

    }
}
