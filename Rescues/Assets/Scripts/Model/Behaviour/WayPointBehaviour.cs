using UnityEngine;


namespace Rescues
{
    [ExecuteInEditMode]
    public class WayPointBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RouteData _routeData;
        [SerializeField] private bool _isScanScene;

        #endregion




        private void Update()
        {
            if (_isScanScene)
            {
                var wayPointBehaviours = FindObjectsOfType<WayPointBehaviour>();

                for (int i = 0; i < wayPointBehaviours.Length; i++)
                {
                    // if wayPointBehaviours[i].GetRouteData() != _routeData} delete
                }
                // sort wayPointBehaviours[i].numOfWayPoint

                _routeData.SetWayPoints(wayPointBehaviours);
                CustomDebug.Log("Scanned for: " + wayPointBehaviours.Length + " objects");
                _isScanScene = false;
            }
        }

        //public RouteData GetRouteData()
    }
}