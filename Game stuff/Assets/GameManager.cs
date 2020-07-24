using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoint;
    int activeSpawnPoint = 0;
    public int spawnedEnemiesAtEachSpawnPoint=10;
    public float waitTimeBetweenEnemies;
    public GameObject [] anEnemy;
    bool timeToSpawn = true;

    public int enemiesKilled = 0;
    public Text killStreakText;
    public List<GameObject> noOfActiveEnemies = new List<GameObject>();
    public int minEnemiesBeforeNewWave = 2;
    bool timeForNewWave=false;

    public Slider playerHealth;
    PlayerMovement playerMovement;

    void Start()
    {
        enemiesKilled = 0;
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHealth.maxValue = playerMovement.maxlives;
    }

    void Update()
    {
        if (timeToSpawn)
        {
            StartCoroutine(SpawnEnemies());
            timeToSpawn = false;
        }

        killStreakText.text = enemiesKilled + " exterminated";
        playerHealth.value = playerMovement.lives;

        if (timeToSpawn == false)
        {
            if (timeForNewWave == true)
            {
                if (noOfActiveEnemies.Count < minEnemiesBeforeNewWave)
                {
                    timeToSpawn = true;
                    timeForNewWave = false;
                    //if there are less than 2 enemies then spawnNewWaveOfEnemies
                }
            }
        }
    }
    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < spawnedEnemiesAtEachSpawnPoint; i++)
        {
            activeSpawnPoint = Random.Range(0, spawnPoint.Length);
            yield return new WaitForSeconds(waitTimeBetweenEnemies);
            int ranndomizeEnemyTypes = Random.Range(0, anEnemy.Length);
            GameObject latestAddition= Instantiate(anEnemy[ranndomizeEnemyTypes], spawnPoint[activeSpawnPoint].transform.position, spawnPoint[activeSpawnPoint].transform.rotation);
            noOfActiveEnemies.Add(latestAddition);
        }
        timeForNewWave = true;
    }
    public void GameOver()
    {
        //Restart Screen with your points 
        Debug.Log("GameOver");
    }
}
