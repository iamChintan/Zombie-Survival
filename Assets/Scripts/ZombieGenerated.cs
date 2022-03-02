using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerated : MonoBehaviour
{
    public GameObject enemy;
    int xPos;
    int zPos;
    int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEney());
    }
    IEnumerator SpawnEney()
    {
        while (enemyCount < 5)
        {
            xPos = Random.Range(30, -30);
            zPos = Random.Range(25, -30);
            Instantiate(enemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount++;
            Debug.Log(enemyCount);
        }

    }
}
