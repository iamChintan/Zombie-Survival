using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerater : MonoBehaviour
{
    public static ZombieGenerater Instance;

    public GameObject[] spawnPositions;
    public GameObject enemy;
    internal int enemyCount;
    float numOfSpawn = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    public IEnumerator SpawnEnemy()
    {
        while (enemyCount < numOfSpawn)
        {
            Instantiate(enemy, spawnPositions[Random.Range(0, spawnPositions.Length - 1)].transform.localPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount++;
            Debug.Log("Count :" + enemyCount);
        }

    }
}
