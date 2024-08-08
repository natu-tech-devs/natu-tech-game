using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(StatusEffect))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Defender))]
public class Player : MonoBehaviour
{
    public Health health;
    public Attacker attacker;
    public Defender defender;

    public StatusEffect statusEffect;
    public GameObject sphere;

    private bool canAttack = true;

    [SerializeField]
    private float launchForce = 10f;
    [SerializeField]
    private float trajectoryTimeStep = 0.1f;
    [SerializeField]
    private int trajectorySegmentCount = 30;
    [SerializeField]
    private GameObject pointPrefab;  // Prefab do ponto da trajetória
    private List<GameObject> trajectoryPoints = new List<GameObject>();

    void Awake()
    {
        if (GameManager.player == null)
        {
            GameManager.player = this;
        }

        health = GetComponent<Health>();
    }

    public void playerInputAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            sphereAttack();
        }
        else if (callbackContext.canceled)
        {
            ClearTrajectory();  // Limpa a trajetória quando o botão é liberado
        }
        else if (callbackContext.started)
        {
            ShowTrajectory();
        }
    }

    void ShowTrajectory()
    {
        ClearTrajectory();  // Limpa os pontos anteriores

        Vector3 velocity = transform.forward * launchForce;
        Vector3 currentPosition = transform.position + transform.forward * 2;

        for (int i = 0; i < trajectorySegmentCount; i++)
        {
            // Instancia um ponto ao longo da trajetória
            GameObject point = Instantiate(pointPrefab, currentPosition, Quaternion.identity);
            trajectoryPoints.Add(point);

            // Calcula a próxima posição baseada na física
            currentPosition += velocity * trajectoryTimeStep;
            velocity += Physics.gravity * trajectoryTimeStep;
        }
    }

    void ClearTrajectory()
    {
        // Destroi todos os pontos anteriores da trajetória
        foreach (var point in trajectoryPoints)
        {
            Destroy(point);
        }
        trajectoryPoints.Clear();
    }

    public void sphereAttack()
    {
        if (!canAttack) return;

        ClearTrajectory();  // Limpa a trajetória ao arremessar
        GameObject instance = Instantiate(sphere);
        if (instance)
        {
            canAttack = false;
            instance.transform.position = transform.position + transform.forward * 2;
            Rigidbody rb = instance.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * launchForce;

            StartCoroutine(AttackCooldown(() =>
            {
                canAttack = true;
                GameManager.gm.endPlayerTurn();
            }));
        }
    }

    IEnumerator AttackCooldown(Action onComplete)
    {
        yield return new WaitForSeconds(1f); // Tempo de recarga do ataque
        onComplete?.Invoke();
    }

    void Start()
    {
        GameManager.gm.turnPrep += turnPrep;

        health = GetComponent<Health>();
        attacker = GetComponent<Attacker>();
        defender = GetComponent<Defender>();
        statusEffect = GetComponent<StatusEffect>();

        defender.onAttack += (Attack attack) =>
        {
            Debug.Log("Player defense");
            health.takeDamage(attack.damage);
        };

        health.onDamage += () =>
        {
            Debug.Log("Player Health: " + health.health);
        };
    }

    void turnPrep()
    {
        Debug.Log("player turn prep");
    }
}
