using UnityEngine;
using System;
using System.Collections.Generic;

namespace Rescues
{
    public class WayPointBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RouteData _routeData;
        [SerializeField] private bool _isScanScene;
        [SerializeField] private int queueOfWaipoint;
        [SerializeField] private float _waitTime;
        [SerializeField] private TrapBehaviour _activatingTrap;
        [SerializeField] private BaseTrapData _baseTrapData;

        public int CompareTo(WayPointBehaviour other)
        {
            return other.queueOfWaipoint.CompareTo(queueOfWaipoint);
        }


        #endregion

        #region Properte

        public TrapBehaviour ActivatingTrap => _activatingTrap;

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
                _isScanScene = false;
            }
        }

        public float GetWaitTime()
        {
            return _waitTime;
        }
    }
}