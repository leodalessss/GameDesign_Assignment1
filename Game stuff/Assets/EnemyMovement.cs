using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
   public NavMeshAgent navMeshAgent;
    public float radius=3;
    public float range = 4;
    float timeBetweenShots = 1.5f;
    float shootingTimer;
    public GameObject projectile;
    public Transform projectileSpawnPt;

    public AudioSource myAudioSource;
    public AudioClip [] myAudioClips;
    int rand;
    public float lives=5;
    GameManager gameManager;
    bool changeSPeed=false;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();
        rand = Random.Range(0, myAudioClips.Length);
        myAudioSource.clip = myAudioClips[rand];
    }

    // Update is called once per frame
    void Update()
    {
       
        SetDestination();
        #region shooting
        if (Vector3.Distance(transform.position, player.position) <= range)
        {
            StartShooting();
        }
        #endregion

        if (lives <= 0)
        {
            //get out a few particles
            gameManager.noOfActiveEnemies.Remove(gameObject);
            gameManager.enemiesKilled++;
            Destroy(gameObject);
        }
        if (navMeshAgent.speed > 4.5)
        {
            timeBetweenShots = 1f;
        }
        shootingTimer += Time.deltaTime;
      /*  if (gameManager.timeForNewWave == true)
        {
            
            navMeshAgent.speed = navMeshAgent.speed + (0.5f * gameManager.waveNumber);
        }
        */
    }
    void SetDestination()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
            navMeshAgent.stoppingDistance = radius;
          
        }
    }
    void StartShooting()
    {
        if (shootingTimer >= timeBetweenShots)
        {
            Instantiate(projectile, projectileSpawnPt.transform.position,projectileSpawnPt.rotation);
            myAudioSource.Play();
            Debug.Log("        WWWWWWWWWWWWWWWW                  AAAAAAAAAAAA0");
            shootingTimer = 0;
        }
    }

  /*  private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            lives--;
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            lives--;
        }
    }
    public void AdjustSpeed()
    {
        navMeshAgent.speed = navMeshAgent.speed + (0.5f * gameManager.waveNumber);
    }
}
