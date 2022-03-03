using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrutible : MonoBehaviour
{
    public GameObject destroyableGO;
    public void onDestroye()
    {
        Instantiate(destroyableGO, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
