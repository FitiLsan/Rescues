using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;




public class SignalingBehaviour : MonoBehaviour
{
    public AudioClip Hitsound;
   private AudioSource _audio;


    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //событие
            _audio.PlayOneShot(Hitsound);
        }
    }
}