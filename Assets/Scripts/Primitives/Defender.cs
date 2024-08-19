using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void onAttack(Attack attack);

public class Defender : MonoBehaviour
{
    public float defenseRatio = 1;
    public onAttack onAttack;

    //TODO grace time
}
