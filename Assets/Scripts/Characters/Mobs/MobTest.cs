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

    [SerializeField]
    private GameObject summon;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject sphere;

    private float summonOffset = 0f;


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

        GameManager.gm.turnManager.addCurrentTurnAction(new TurnAction()
        {
            validation = () =>
            {
                bool result = !GameManager.gm.getPlayerSuccess;
                Debug.Log("succes: " + result);
                return result;
            },
            action = turnAction
        });
    }



    public IEnumerator turnAction()
    {
        if (Random.Range(0, 2) > 0)
            yield return attackPlayer();
        else
            yield return summonMob();

        yield return null;
    }


    private IEnumerator attackPlayer()
    {
        var instance = Instantiate(sphere).GetComponent<Sphere>();

        if (instance != null)
        {
            Vector3 playerPos = GameManager.player.transform.position;
            Vector3 playerDirection = (playerPos - transform.position).normalized;
            instance.transform.position = transform.position + playerDirection * 8;
            Vector3 launchForce = ProjectileLauncher.CalculateLaunchForce(instance.transform.position, playerPos, 60f);
            instance.launch(launchForce, attacker);
        }
        yield return null;
    }


    private IEnumerator summonMob()
    {
        var instance = Instantiate(summon, spawnPoint);
        instance.transform.position = spawnPoint.transform.position + spawnPoint.transform.forward * summonOffset;
        summonOffset += 2f;
        yield return null;
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
