using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnAction 
{
    public Func<bool> validation = () => true;
    public Func<IEnumerator> action;

    public IEnumerator takeAction(){
        if(validation()) yield return action();
    }
}
