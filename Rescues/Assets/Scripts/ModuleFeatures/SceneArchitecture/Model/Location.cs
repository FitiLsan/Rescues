using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Rescues
{
    public class Location : MonoBehaviour
    {

        #region Filed

        [SerializeField] private Transform _cameraPosition;

        #endregion
        
        
        #region Properties
        
        public Vector3 CameraPosition => _cameraPosition.position;
        public List<CurveWay> СurveWays { get; private set; }

        #endregion
        
        
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