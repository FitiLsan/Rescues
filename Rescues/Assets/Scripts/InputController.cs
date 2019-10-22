using UnityEngine;

public class InputController : IOnUpdate
{
    private Vector3 Direction;
    public Character Character;

    public void OnUpdate()
    {
        Direction.x = Input.GetAxis("Horizontal");

        if(Direction.x != 0)
        {
            Character._characterDirection.x = Direction.x;
            Character.Move();
        }

        

    }
}