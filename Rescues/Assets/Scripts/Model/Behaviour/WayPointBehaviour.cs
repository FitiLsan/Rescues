using UnityEngine;


namespace Rescues
{
    [ExecuteInEditMode]
    public class WayPointBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RouteData _routeData;
        [SerializeField] private float _waitTime;
        [SerializeField] private TrapInfo _activatingTrap;
        [SerializeField] private bool _isScanScene;
        [SerializeField] private BaseTrapData _baseTrapData;

        #endregion

        #region Properte

        public TrapInfo ActivatingTrap => _activatingTrap;

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

        public float GetWaitTime()
        {
            return _waitTime;
        }
    }
}