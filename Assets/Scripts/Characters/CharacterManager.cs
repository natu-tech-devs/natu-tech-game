using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CharacterManager
{
    static private Player _player;
    static public Player player {
        get  {
            if(_player == null){
                _player = GameObject.FindAnyObjectByType<Player>();
            }
            return _player;
        }
        set {
            _player = value;
        }
    }


    public static void PlayerAttack(ICharacter target, Attack attack){
        if(player != null)
            CharacterManager.Attack(_player,target,attack);
        else 
            throw new System.Exception("Player Not Found");
    }

    public static void Attack(ICharacter attacker,ICharacter target, Attack attack){
        attacker.attack(attack);
        target.getAttacked(attack);
    }
    public static void AttackGroup(ICharacter atacker, ICharacter[] targets,Attack attack){
        atacker.attack(attack);
        foreach(ICharacter target in targets){
            target.getAttacked(attack);
        }
    }
}
