using System;
using UnityEngine;


namespace Rescues
{
    public sealed class EnemyBehaviour : MonoBehaviour // TODO отрефакторить под конвенцию
    {
        public EnemyData EnemyData;
        public int PatrolPointState;
        public RouteData RouteData;

        private PhysicsService _physicsService;
        private Vector3 _visionDirection;
        [SerializeField] private bool _isWaiting;

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
                ScreenInterface.GetInstance().Execute(ScreenType.GameOver);
            }
        }

        public void SetVisionDirection(Vector3 visionDirection)
        {
            _visionDirection = visionDirection.normalized;
        }

        public bool GetWaitState()
        {
            return _isWaiting;
        }

        public void SetWaitState(bool isWaiting)
        {
            _isWaiting = isWaiting;
        }

        internal bool CheckWaitTime(float waitTime)
        {
            if (!_isWaiting)
            {
                if (waitTime > 0)
                {
                    TimeRemaining timeRemaining = new TimeRemaining (ResetWaitState, waitTime);
                    _isWaiting = true;
                }
            }

            return _isWaiting;
        }

        private void ResetWaitState()
        {
            _isWaiting = false;
        }
    }
}
