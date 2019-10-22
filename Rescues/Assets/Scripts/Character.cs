using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector3 _characterDirection;
    [SerializeField]  float _characterSpeed;    
    
    public void Move()
    {
        transform.position += _characterDirection * _characterSpeed * Time.deltaTime;
    }
}
