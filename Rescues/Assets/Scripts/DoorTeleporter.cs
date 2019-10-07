using UnityEngine;
using System.Collections;

public class DoorTeleporter : MonoBehaviour
{
    [SerializeField] Transform upperDoor;
    [SerializeField] Transform lowerDoor;


    public void JumpUp(Transform go)
    {
        if(upperDoor != null) go.transform.position = upperDoor.position;
    }

    public void JumpDown(Transform go)
    {        
        if (lowerDoor != null) go.transform.position = lowerDoor.position;
    }
}
