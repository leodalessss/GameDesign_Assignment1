using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    AudioSource myAudioSource;
    int rand;
    public AudioClip[] myAudioClip;

    void Start()
    {
        myAudioSource = this.GetComponent<AudioSource>();
        rand = Random.Range(0, myAudioClip.Length);
        myAudioSource.clip = myAudioClip[rand];
        myAudioSource.Play();
    }
}
