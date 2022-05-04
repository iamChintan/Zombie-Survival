using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    [SerializeField] private float throwForce = 3f;
    [SerializeField] private GameObject granade;
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowGrenade();
        }
    }

    /// <summary>
    /// this method is used to throw granade at particular position
    /// </summary>
    private void ThrowGrenade()
    {
        GameObject gb= Instantiate(granade, transform.position, transform.rotation);
        Rigidbody rb = gb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        rb.useGravity = true;
        Destroy(gb, 4f);

    }
}
