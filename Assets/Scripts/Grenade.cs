using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public GameObject explotion;
    float countDown;
    public Camera fpsCam;
    bool hasExploded = false;
    public float range = 100f;
    public float damage = 10f;

    void Start()
    {
        countDown = delay;
    }

    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        RaycastHit hit;
        

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Target target = hit.transform.GetComponent<Target>();
            //if (target != null)
            //{
            //    target.TakeDamage(damage);
            //}
            //if (hit.rigidbody != null)
            //{
            //    hit.rigidbody.AddForce(-hit.normal * shootForce);
            //}

            //GameObject hitflasheffect = Instantiate(hitFlash, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(hitflasheffect, 2f);



            Instantiate(explotion, hit.transform.position, hit.transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(hit.transform.position, radius);
            foreach (var item in colliders)
            {
                Rigidbody rb = item.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, hit.transform.position, radius);
                }
            }

            Destroy(gameObject);
        }

    }
}
