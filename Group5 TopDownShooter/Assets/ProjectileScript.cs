using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
   public  float force=30f;
    Rigidbody projRb;
    // Start is called before the first frame update
    void Start()
    {
        projRb = GetComponent<Rigidbody>();
        projRb.AddForce(transform.up * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
