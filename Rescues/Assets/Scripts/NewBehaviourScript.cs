using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject ground;
    public GameObject plane;
    public Vector3 one = new Vector3(1, 5, 0);
    public Vector3 two = new Vector3(1, 0, 0);
    public float Distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        one = ground.transform.rotation.eulerAngles;
        two = plane.transform.rotation.eulerAngles;

        Debug.DrawRay(ground.transform.position, ground.transform.up * Distance, Color.red);
        Debug.DrawRay(plane.transform.position, plane.transform.forward * Distance, Color.red);

        //print(Vector3.Dot(one, two));
        print(Vector3.Dot(ground.transform.up, plane.transform.forward));
    }
}
