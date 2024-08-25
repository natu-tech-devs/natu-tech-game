using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TurnAction 
{
    public Func<bool> validation = () => true;
    public Func<IEnumerator> action;

    public Func<bool> cleanUp = () => false;

    public float waitingTime = 1f;

    public IEnumerator takeAction(){
        if(cleanUp()) {
            action = null;
            validation = null;
            yield return null;
        }
        if(validation !=null && validation()) yield return action();
        else yield break;//dont await for end of action if validation dont pass
        yield return new WaitForSeconds(waitingTime);
    }
}
