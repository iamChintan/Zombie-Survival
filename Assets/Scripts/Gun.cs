using System;
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float shootForce = 50f;
    public float firerate = 15f;
    public float radious = 5f;
    public float force = 500f;

    public Camera fpsCam;
    public ParticleSystem flash;
    public GameObject hitFlash;
    public GameObject ScopeOverlay;
    public GameObject weaponCamera;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    public Animator reloadingAnimation;

    public GameObject explosionEffext;

    private float nextFireTime = 0f;
    private bool isReloading = false;
    internal bool isZoomed = false;
    private float scopedFov = 15f;
    private float normalFov;


    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        reloadingAnimation.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / firerate;
            Shoot();
        }

        if (Input.GetButtonDown("Fire2") && transform.name == "Sniper")
        {
            isZoomed = !isZoomed;
            reloadingAnimation.SetBool("SniperZoom", isZoomed);
            if (isZoomed) StartCoroutine(Scoped());
            else UnScoped();
        }
    }

    IEnumerator Scoped()
    {
        yield return new WaitForSeconds(0.15f);
        normalFov = fpsCam.fieldOfView;
        fpsCam.fieldOfView = scopedFov;
        ScopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
    }

    public void UnScoped()
    {
        Debug.Log("UnScoped..!");
        ScopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        fpsCam.fieldOfView = normalFov;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading..!");
        if (transform.name == "Sniper" && isZoomed)
        {
            reloadingAnimation.SetBool("SniperZoom", false);
            UnScoped();
            yield return new WaitForSeconds(.15f);
        }
        reloadingAnimation.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime-0.25f);
        reloadingAnimation.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
        if (transform.name == "Sniper" && isZoomed)
        {
            reloadingAnimation.SetBool("SniperZoom", true);
            StartCoroutine(Scoped());
        }
    }

    private void Shoot()
    {
        flash.Play();
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (hit.transform.name.Contains("RedBarrel"))
            {
                GameObject go =  Instantiate(explosionEffext, hit.transform.position, hit.transform.rotation);
                
                Collider[] colliders = Physics.OverlapSphere(hit.transform.position, radious);
                foreach (Collider nearObj in colliders)
                {
                   
                    Destrutible destb = nearObj.GetComponent<Destrutible>();
                    if (destb != null)
                    {
                        destb.onDestroye();
                    }
                }

                Collider[] colliders1 = Physics.OverlapSphere(hit.transform.position, radious);
                foreach (Collider nearObj in colliders1)
                {
                    Rigidbody rb = nearObj.GetComponent<Rigidbody>();
                    if (rb != null && rb.name.Contains("Zombie"))
                    {
                        target = rb.transform.GetComponent<Target>();
                        if (target!= null)
                        {
                            Debug.Log("BlastDie Calling");
                            target.BlastDie();
                        }

                        Vector3 v = new Vector3(force,0,0);
                        rb.AddForce(v);
                    }
                }
 
                Destroy(go, 3f);
            }

            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * shootForce);
            }

            GameObject hitflasheffect = Instantiate(hitFlash, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitflasheffect, 2f);
        }
    }
}
