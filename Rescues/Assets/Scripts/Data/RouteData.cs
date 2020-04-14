using UnityEngine;

namespace Rescues
{
    [CreateAssetMenu(fileName = "RouteData", menuName = "Data/NPC/RouteData")]
    public sealed class RouteData : ScriptableObject
    {
        [SerializeField] private Vector3[] WayPoints;

        public void SetWayPoints(WayPointBehaviour[] wayPointBehaviours)
        {
            WayPoints = new Vector3[wayPointBehaviours.Length];

            for (int i = 0; i < wayPointBehaviours.Length; i++)
            {
                WayPoints[i] = wayPointBehaviours[i].transform.position;
            }
        }

        public Vector3[] GetWayPoints()
        {
            return WayPoints;
        }
    }
}