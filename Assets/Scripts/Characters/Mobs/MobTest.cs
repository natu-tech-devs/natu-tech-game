using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTest : MonoBehaviour
{
    private int test = 0;
    void turnPrep(){
        Debug.Log("test turn prep: "  + test++);
        GameManager.gm.turnManager.addCurrentTurnAction(new() {
            action = turnAction,
            validation = true
        });
    }

    void turnAction(){
        Debug.Log("Mob Turn Action");
    }
    void Start(){
        GameManager.gm.turnPrep += turnPrep;
    }
}
