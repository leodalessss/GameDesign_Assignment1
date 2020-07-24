using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public  float force=30f;
    public GameObject myParticleSystem;
    Rigidbody projRb;

    void Start()
    {
        projRb = GetComponent<Rigidbody>();
        projRb.AddForce(transform.up * force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(myParticleSystem, this.transform);
        StartCoroutine(SmallWait());
        Destroy(this.transform.GetChild(0).gameObject);
    }

    private IEnumerator SmallWait()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
