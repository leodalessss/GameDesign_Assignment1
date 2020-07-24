using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


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
    public int waveNumber = 0;

    public Slider playerHealth;
    PlayerMovement playerMovement;

    public float enemySPeedIncrements = 0.5f;

    public AudioListener playerAudioListener;
    public GameObject endPanel;
    public Text score;
    void Start()
    {
        playerAudioListener.enabled = true;
        endPanel.SetActive(false);
        enemiesKilled = 0;
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHealth.maxValue = playerMovement.maxlives;
        waveNumber = 0;
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
                    waveNumber++;               
                    timeForNewWave = false;
                    //if there are less than 2 enemies then spawnNewWaveOfEnemies
                }
            }
        }
        if (waveNumber > 3)
        {
            waveNumber = 3;
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
             latestAddition.GetComponent<EnemyMovement>().navMeshAgent.speed = latestAddition.GetComponent<EnemyMovement>().navMeshAgent.speed+(enemySPeedIncrements*waveNumber);
           
            noOfActiveEnemies.Add(latestAddition);
        }
        timeForNewWave = true;
       
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        //Restart Screen with your points 
        playerAudioListener.enabled = false;
        endPanel.SetActive(true);
        Debug.Log("GameOver");
        score.text = "Score: " + enemiesKilled.ToString();
    }
}
