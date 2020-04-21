using UnityEngine;
using System.Collections.Generic;

namespace Rescues
{
    [ExecuteInEditMode]
    public class WayPointBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RouteData _routeData;
        [SerializeField] private bool _isScanScene;
        [SerializeField] private int queueOfWaipoint;

        #endregion




        private void Update()
        {
            if (_isScanScene)
            {
                var wayPointBehaviours = FindObjectsOfType<WayPointBehaviour>();
                List<WayPointBehaviour> listWayPointBehaviours = new List<WayPointBehaviour>();
                for (int i = 0; i < wayPointBehaviours.Length; i++)
                {
                    if (wayPointBehaviours[i]._routeData == _routeData)
                    {
                        listWayPointBehaviours.Add(wayPointBehaviours[i]);
                    }
                }
                listWayPointBehaviours.Sort();
                _routeData.SetWayPoints(listWayPointBehaviours.ToArray());
                CustomDebug.Log("Scanned for: " + wayPointBehaviours.Length + " objects");
                _isScanScene = false;
            }
        }

    }
}