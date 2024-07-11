using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Sphere sphere;
    [SerializeField]
    private float shootSpeed = 10f;

    void Start(){
        CharacterManager.player = this;
    }

    public void sphereAttack(){
        GameObject shoot =  Instantiate(sphere.gameObject, transform.position,Quaternion.identity);
        shoot.GetComponent<Rigidbody>().AddForce(transform.forward * shootSpeed);
    }

    public override void attack(Attack attack)
    {
        base.attack(attack);
    }
}
