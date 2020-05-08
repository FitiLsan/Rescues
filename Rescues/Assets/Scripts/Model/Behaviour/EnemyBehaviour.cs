using UnityEngine;

namespace Rescues
{
    public sealed class EnemyBehaviour : MonoBehaviour
    {
        public EnemyData EnemyData;
        public int PatrolPointState;
        public RouteData RouteData;

        private PhysicsService _physicsService;
        private Vector3 _visionDirection;

        private void Awake()
        {
            _physicsService = Services.SharedInstance.PhysicsService;
        }

        public int Modificator { get => _modificator; }

        private int _modificator = 1;

        public void InvertModificator()
        {
            _modificator *= -1;
        }

        public void Vision()
        {
            var hit = _physicsService.VisionDetectionPlayer(transform.position, _visionDirection, EnemyData.VisionDistance);

            if (hit != false)
            {
                CustomDebug.Log("Defeat");
            }
        }

        public void SetVisionDirection(Vector3 visionDirection)
        {
            _visionDirection = visionDirection.normalized;
        }
    }
}
