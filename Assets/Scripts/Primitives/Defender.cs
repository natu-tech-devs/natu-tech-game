using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void onAttack(Attack attack,AgentSide agentSide = AgentSide.NEUTRAL);

public class Defender : MonoBehaviour
{
    public float defenseRatio = 1;
    public onAttack onAttack;

    public AgentSide agentSide = AgentSide.NEUTRAL;

    //TODO grace time
}
