using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private bool hasTimeSpan = true;

    [SerializeField]
    private int timeSpan = 3;

    void Start()
    {
        if(hasTimeSpan){
            GameManager.gm.turnManager.addTurnAction(new TurnAction() {
                action = die,
                validation = () => this != null,
            },timeSpan);
        }
    }

    private IEnumerator die(){
        Destroy(gameObject);
        yield return null;
    }

}
