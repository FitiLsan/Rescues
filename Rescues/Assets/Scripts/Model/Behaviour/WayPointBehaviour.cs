using UnityEngine;
using System;
using System.Collections.Generic;

namespace Rescues
{
    [ExecuteInEditMode]
    public class WayPointBehaviour : MonoBehaviour, IComparable <WayPointBehaviour>
    {
        #region Fields

        [SerializeField] private RouteData _routeData;
        [SerializeField] private bool _isScanScene;
        [SerializeField] private int queueOfWaipoint;

        public int CompareTo(WayPointBehaviour other)
        {
            return other.queueOfWaipoint.CompareTo(queueOfWaipoint);
        }


        #endregion




        private void Update()
        {
            if (_isScanScene)
            {
                var wayPointBehaviours = FindObjectsOfType<WayPointBehaviour>();
                List<WayPointBehaviour> listWayPointBehaviours = new List<WayPointBehaviour>();
                CustomDebug.Log(wayPointBehaviours.Length);
                for (int i = 0; i < wayPointBehaviours.Length; i++)
                {
                    if (wayPointBehaviours[i]._routeData == _routeData)
                    {
                        listWayPointBehaviours.Add(wayPointBehaviours[i]);
                    }
                }
                CustomDebug.Log(listWayPointBehaviours.Count);
                listWayPointBehaviours.Sort();
                _routeData.SetWayPoints(listWayPointBehaviours.ToArray());
                CustomDebug.Log("Scanned for: " + wayPointBehaviours.Length + " objects");
                _isScanScene = false;
            }
        }

    }
}