using UnityEngine;
using System;




public delegate void MyEventHandler();
public class Menedger : MonoBehaviour
{


    public static AudioClip Hitsound;
    public static AudioSource Audio;

    public event MyEventHandler SomeEvent;


    public void OnSomeEvent()
    {
        if (SomeEvent != null)
            SomeEvent();
    }

   public static void Handler()
    {

        Audio.PlayOneShot(Hitsound);
    }




    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Menedger evt = new Menedger();
        evt.SomeEvent += Handler;
    }

   


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //событие
            OnSomeEvent();

            
            
        }
    }
}