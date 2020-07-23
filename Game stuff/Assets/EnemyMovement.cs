using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    NavMeshAgent navMeshAgent;
    public float radius=3;
   public float range = 4;
    float timeBetweenShots = 1.5f;
    float shootingTimer;
   public GameObject projectile;
   public Transform projectileSpawnPt;

    public float lives=5;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();

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
        shootingTimer += Time.deltaTime;
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

            shootingTimer = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            lives--;
        }
    }
}
