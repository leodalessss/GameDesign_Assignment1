using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public  float force=30f;
    public GameObject myParticleSystem;

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
        Instantiate(myParticleSystem, this.transform);
        StartCoroutine(SmallWait());
        this.GetComponent<MeshRenderer>().enabled = false;

    }

    private IEnumerator SmallWait()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
