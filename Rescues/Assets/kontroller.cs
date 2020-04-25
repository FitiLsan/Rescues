using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kontroller : MonoBehaviour
{
    public float speed = 30f;

    Vector3 dir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        dir.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += dir;


        
    }
}
