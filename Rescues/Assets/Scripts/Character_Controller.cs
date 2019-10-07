using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 dir = Vector3.zero;
    Rigidbody2D myRg;

    bool isForward = true;

    DoorTeleporter door;

    private void Start()
    {
        myRg = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {        
        dir.x = Input.GetAxis("Horizontal") * speed;
        dir.y = myRg.velocity.y;

        myRg.velocity = dir;

        if (myRg.velocity.x > 0 && !isForward) Flip();
        else if(myRg.velocity.x < 0 && isForward) Flip();

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (door != null) door.JumpUp(transform);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (door != null) door.JumpDown(transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Door")
        {
            door = collision.GetComponent<DoorTeleporter>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            if(door == collision.GetComponent<DoorTeleporter>()) door = null;
        }
    }

    void Flip()
    {
        isForward = !isForward;
        Vector3 dir = transform.localScale;
        dir.x *= -1;
        transform.localScale = dir;
    }
}
