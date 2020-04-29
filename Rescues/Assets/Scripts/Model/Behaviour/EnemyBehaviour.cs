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
        [SerializeField] private StateEnemy _stateEnemy;

        private void Awake()
        {
            _physicsService = Services.SharedInstance.PhysicsService;
            _timeRemaining = new TimeRemaining (ResetWaitState, 0.0f);
        }

        public int Modificator { get => _modificator; }

        private int _modificator = 1;
        private TimeRemaining _timeRemaining;

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

        public StateEnemy GetWaitState()
        {
            return _stateEnemy;
        }

        private void SetInspectionState()
        {
            if (_stateEnemy == StateEnemy.Patrol)
            {
                _stateEnemy = StateEnemy.Inspection;
            }
        }

        public void WaitTime(float waitTime)
        {
            SetInspectionState();
            _timeRemaining.AddTimeRemaining(waitTime);
        }

        private void ResetWaitState()
        {
            _stateEnemy = StateEnemy.Patrol;
            PatrolPointState += Modificator;
        }
    }
}
