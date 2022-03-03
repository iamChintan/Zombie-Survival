using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 3f;
    public GameObject granade;
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {
       GameObject gb= Instantiate(granade, transform.position, transform.rotation);
        Rigidbody rb = gb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        rb.useGravity = true;

    }
}
