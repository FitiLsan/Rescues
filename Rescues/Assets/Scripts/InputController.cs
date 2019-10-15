using UnityEngine;

public class InputController : IOnUpdate
{
    public Vector3 Dir;
    public float Speed = 2;

    public void OnUpdate()
    {
        Dir.x = Input.GetAxis("Horizontal");

        if(Dir.x != 0)
        {
            Main.Instance.Player.transform.position += Dir * Speed * Time.deltaTime;
        }
    }
}