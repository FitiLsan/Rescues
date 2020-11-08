using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public class Location : MonoBehaviour
    {

        [SerializeField] private Transform _cameraPosition;
        [SerializeField] private List<CurveWay> _curveWays;
        
        
        public Vector3 CameraPosition => _cameraPosition.position;
        public List<CurveWay> СurveWays => _curveWays;

        public void Destroy()
        {
            Destroy(gameObject);
        }

    }
    
}