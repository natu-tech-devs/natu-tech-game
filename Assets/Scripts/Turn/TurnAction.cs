using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAction : MonoBehaviour
{
    public bool validation = true;
    public Action  action;

    public void takeAction(){
        if(validation) action();
    }
}
