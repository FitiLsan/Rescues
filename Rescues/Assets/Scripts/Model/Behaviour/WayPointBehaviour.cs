using UnityEngine;

namespace Rescues
{

    [ExecuteInEditMode]
    public class WayPointBehaviour : MonoBehaviour
    {
        [SerializeField] private RouteData _routeData;
        [SerializeField] private bool _isScanScene;

        private void Update()
        {
            if (_isScanScene)
            {
                var wayPointBehaviours = FindObjectsOfType<WayPointBehaviour>();
                // if wayPointBehaviours[i].GetRouteData() != _routeData} delete
                // sort wayPointBehaviours[i].numOfWayPoint

                _routeData.SetWayPoints(wayPointBehaviours);
                CustomDebug.Log("Scanned for: " + wayPointBehaviours.Length + " objects");
                _isScanScene = false;
            }
        }

        //public RouteData GetRouteData()
    }
}