using UnityEngine;


namespace Rescues
{
    public class Location : MonoBehaviour
    {

        [SerializeField] private Transform _cameraPosition;

        public Vector3 CameraPosition => _cameraPosition.position;

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}