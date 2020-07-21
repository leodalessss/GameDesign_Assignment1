using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoint;
    int activeSpawnPoint = 0;
    public int spawnedEnemiesAtEachSpawnPoint=10;
    public float waitTimeBetweenEnemies;
    public GameObject anEnemy;
    bool timeToSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToSpawn)
        {
            StartCoroutine(SpawnEnemies());
            timeToSpawn = false;
            
        }

    }
    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < spawnedEnemiesAtEachSpawnPoint; i++)
        {
            yield return new WaitForSeconds(waitTimeBetweenEnemies);
            Instantiate(anEnemy, spawnPoint[activeSpawnPoint].transform.position, spawnPoint[activeSpawnPoint].transform.rotation);
            
            Debug.Log(i);
        }
       
    }
}
