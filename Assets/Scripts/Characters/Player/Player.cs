using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Awake(){
        if(GameManager.player == null){
            GameManager.player = this;
        }
    }
}
