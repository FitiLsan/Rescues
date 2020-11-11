using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Rescues
{
    public class Location : MonoBehaviour
    {

        [SerializeField] private Transform _cameraPosition;


        public Vector3 CameraPosition => _cameraPosition.position;
        public List<CurveWay> СurveWays { get; private set; }

        
        
        #region UnityMethods
        
        private void Awake()
        {
            СurveWays = transform.GetComponentsInChildren<CurveWay>().ToList();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion
        
        
    }
    
}