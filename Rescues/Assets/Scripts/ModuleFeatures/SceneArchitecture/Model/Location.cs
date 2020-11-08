using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public class Location : MonoBehaviour
    {

        [SerializeField] private Transform _cameraPosition;
        [SerializeField] private List<CurveWay> _curveWays;
        [SerializeField] private ScalePoint _scalePoint;
        
        public Vector3 CameraPosition => _cameraPosition.position;
        public List<CurveWay> СurveWays => _curveWays;
        public ScalePoint ScalePoint => _scalePoint;

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public CurveWay GetCurve(WhoCanUseWayTypes type)
        {
            var curve = _curveWays.Find(x => x.WhoCanUseWay == type);
            if (curve == null)
                curve = _curveWays.Find(x => x.WhoCanUseWay == WhoCanUseWayTypes.All);

            return curve;
        }
        
    }
    
}