using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SignalingBehaviour : MonoBehaviour
{
    public AudioClip hitsound;
    AudioSource audio;


    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //событие
            audio.PlayOneShot(hitsound);
        }
    }
}