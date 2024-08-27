using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Defender))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(StatusEffect))]
public class Boss : MonoBehaviour
{
    private Health health;
    private Defender defender;
    private StatusEffect statusEffect;

    [SerializeField]
    private ElemetType currentElement;

    [SerializeField]
    private int actionSpeed = 1;
    private int actionCount = 0;

    private int noHitCount = 0;
    private int noHitHealCount = 5;

    [SerializeField]
    private GameObject drone;

    [SerializeField]
    private GameObject truck;

    [SerializeField]
    private Transform droneSpawn;
    [SerializeField]
    private Transform truckSpawn;

    private void Start()
    {
        health = GetComponent<Health>();
        defender = GetComponent<Defender>();
        statusEffect = GetComponent<StatusEffect>();

        defender.onAttack += onAttack;

        GameManager.gm.turnPrep += prep;
    }

    private void onAttack(Attack attack, AgentSide agentSide)
    {
        if (agentSide != defender.agentSide && currentElement != attack.element)
        {
            health.takeDamage(attack.damage);
            noHitCount = 0;
        }
    }

    private IEnumerator turnAction()
    {
        if (noHitCount >= noHitHealCount)
        {
            heal();
            noHitCount = 0;
            yield break;
        }
        yield return summon();
        yield return null;
    }

    private IEnumerator summon()
    {
        var rng = Random.Range(0, 2);
        if (rng == 1)
        {
            yield return summonDrone();
        }
        else
        {
            summonTruck();
        }

    }

    private void summonTruck()
    {
        if (truckSpawn == null || truckSpawn.childCount <= 0) return;

        var spawn = truckSpawn.GetChild(0);
        if (spawn != null)
        {
            Debug.Log("summon truck");
            var truckInstance = Instantiate(truck);
            truckInstance.transform.position = spawn.transform.position;
            Destroy(spawn.gameObject);
        }
    }

    private IEnumerator summonDrone()
    {
        if (droneSpawn == null) yield break;
        if (droneSpawn.childCount <= 0) yield break;
        var spawn = droneSpawn.GetChild(0);
        if (spawn != null)
        {
            Debug.Log("summon drone");
            var droneInstance = Instantiate(drone);
            droneInstance.transform.position = spawn.transform.position;
            Destroy(spawn.gameObject);

            yield return new WaitForEndOfFrame();

            BaseDrone droneScript = droneInstance.GetComponent<BaseDrone>();
            if (droneScript != null)
            {
                StartCoroutine(droneScript.orbitRoutine(transform, droneScript.orbitSpeed));
            }
        }

    }

    private void heal()
    {
        health.heal(10);
    }

    private void prep()
    {
        if (++actionCount >= actionSpeed)
        {
            GameManager.gm.turnManager.addTurnAction(new()
            {
                action = turnAction,
                validation = () => this != null
            });
            actionCount = 0;
        }

    }

    private void OnDestroy()
    {
        GameManager.gm.turnPrep -= prep;
    }
}
