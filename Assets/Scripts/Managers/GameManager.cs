using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private TurnManager turnManager;

    public static Player player;

    public static GameManager gm;

    // Start is called before the first frame update
    private void Awake()
    {
        if (GameManager.gm == null)
        {
            GameManager.gm = this;
        }
    }

    public void startTurn(){
        turnManager.startCurrentTurn();
    }



}
