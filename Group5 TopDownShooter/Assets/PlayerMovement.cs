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

    public int maxBullets;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
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
        if (Input.GetButtonDown("Shoot"))
        {
            Instantiate(projectile, projectilePt.transform.position, projectilePt.rotation);
        }
        if (Input.GetButton("Shoot") )
        {

            StartCoroutine(ShootMultipleBullets()); 

        }
       
        
        #endregion

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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.z, direction.x)*Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(0, -angle, 0);
    }

    IEnumerator ShootMultipleBullets()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            yield return new WaitForSeconds(0.02f);
            Instantiate(projectile, projectilePt.transform.position, projectilePt.rotation);

        }
    }
    }
