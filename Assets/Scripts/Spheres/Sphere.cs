using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Sphere : MonoBehaviour
{

    [SerializeField]
    public Attack attack;

    public UnityEvent onHit;

    public new Rigidbody rigidbody => GetComponent<Rigidbody>();

    IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        if (gameObject)
            GameObject.Destroy(gameObject);
    }


    public void OnCollisionEnter(Collision collision)
    {
    }

}
