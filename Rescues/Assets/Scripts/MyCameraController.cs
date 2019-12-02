using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
//    FlyCamera2D flyCamera;
//    SmoothFollow2DCamera smoothFollow;
//
//	// Use this for initialization
//	void Start ()
//    {
//        flyCamera = GetComponent<FlyCamera2D>();
//        smoothFollow = GetComponent<SmoothFollow2DCamera>();
//    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
//            flyCamera.enabled = true;
//            smoothFollow.enabled = false;
        }

        if(Input.GetKeyUp(KeyCode.LeftAlt))
        {
//            flyCamera.enabled = false;
//            smoothFollow.enabled = true;
        }
    }
}
