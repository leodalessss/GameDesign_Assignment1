using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    int rand;
    float randTime, randTimeCounter;

    public AudioSource myAudioSource;
    public AudioClip[] myAudioClip;

    void Start()
    {
        rand = Random.Range(0, myAudioClip.Length);
        randTime = Random.Range(0, 5f);
        randTimeCounter = 0;

        myAudioSource.clip = myAudioClip[rand];
    }

    private void Update()
    {
        if(randTimeCounter >= randTime)
        {
            if(!myAudioSource.isPlaying)
            {
                randTime = Random.Range(3f, 5f);
                randTimeCounter = 0;
                myAudioSource.Play();
            }
        }
        else
        {
            randTimeCounter += Time.deltaTime;
        }
    }
}
