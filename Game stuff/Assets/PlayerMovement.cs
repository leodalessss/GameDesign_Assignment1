using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed=5f;
    Rigidbody rb;
    bool slowMo=false;
    public Transform projectilePt;
    public GameObject projectile;

    AudioSource myAudioSource;
    #region shootingDecl
    public int maxBullets=10;
    int currentBullets;
    float reloadTime = 0.5f;
    float nextTimeToFire;
    public float fireRate = 10f;
    #endregion

    public int maxlives = 25;
    public int lives = 25;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentBullets = maxBullets;
        lives = maxlives;
        gameManager = FindObjectOfType<GameManager>();
        myAudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        #region SlowMo
        if (Input.GetMouseButtonDown(1))
        {
            slowMo = !slowMo;
           
            if (slowMo == true)
            {
                Time.timeScale = 0.5f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            
        }
        #endregion

        #region shooting
     
        if (Input.GetButton("Shoot") &&Time.time>=nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1.2f / fireRate;
            if (currentBullets > 0)
            {
                MultiShoot();
            }

        }
       if (Input.GetButtonUp("Shoot"))
        {
           currentBullets = maxBullets;
        }

        #endregion

        if (lives <= 0)
        {
            gameManager.GameOver();
        }
    }
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, z)*speed*Time.deltaTime;
        rb.velocity = move;
        FaceTheMouse();        
    }
    
   void FaceTheMouse()
    {
        /* Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         Vector3 direction = mousePosition - transform.position;
         float angle = Mathf.Atan2(direction.z, direction.x)*Mathf.Rad2Deg;
         rb.rotation = Quaternion.Euler(0, -angle, 0);
         */
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float raylength;

        if(groundPlane.Raycast(cameraRay,out raylength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(raylength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y,pointToLook.z));
        }
    }

    void MultiShoot()
    {
        Instantiate(projectile, projectilePt.transform.position, projectilePt.rotation);
        //if(!myAudioSource.isPlaying) If we want only one bullet sound at a time
        {
            myAudioSource.Play();
        }
        currentBullets--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            lives--;
        }
    }
}
