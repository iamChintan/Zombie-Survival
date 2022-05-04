using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float delay = 3f;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float force = 700f;
    [SerializeField] private GameObject explotion;
    private float countDown;
    [SerializeField] private Camera fpsCam;
    private bool hasExploded = false;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 10f;

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
            hasExploded = !hasExploded;
        }
    }

    /// <summary>
    /// this method is use to explode the flamable things such as red barrel.
    /// </summary>
    private void Explode()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = transform.GetComponent<Target>();
            GameObject go = Instantiate(explotion, transform.position, transform.rotation);

            Collider[] colliders1 = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearObj in colliders1)
            {
                Rigidbody rb = nearObj.GetComponent<Rigidbody>();
                if (rb != null && rb.gameObject.tag == "Zombie")
                {
                    target = rb.transform.GetComponent<Target>();
                    if (target != null)
                    {
                        Debug.Log("BlastDie Calling");
                        target.BlastDie();
                    }

                    Vector3 v = new Vector3(force, 0, 0);
                    rb.AddForce(v);
                }
            }
            Destroy(go, 3f);
        }

    }
}
