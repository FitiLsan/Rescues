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

        private void Awake()
        {
            _physicsService = Services.SharedInstance.PhysicsService;
            _timeRemaining = new TimeRemaining (ResetWaitState, 0.0f);
            EnemyData.StateEnemy = StateEnemy.None;
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

            if (hit)
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
            return EnemyData.StateEnemy;
        }

        private void SetInspectionState()
        {
            if (EnemyData.StateEnemy == StateEnemy.Patrol)
            {
                EnemyData.StateEnemy = StateEnemy.Inspection;
            }
        }

        public void WaitTime(float waitTime)
        {
            SetInspectionState();
            _timeRemaining.AddTimeRemaining(waitTime);
        }

        private void ResetWaitState()
        {
            CustomDebug.Log(PatrolPointState);
            var trapInfoBaseTrapData = RouteData.GetWayPoints()[PatrolPointState].TrapInfo.BaseTrapData;
            if (trapInfoBaseTrapData != null)
            {
                trapInfoBaseTrapData.ActivateTrap(EnemyData);
                if (trapInfoBaseTrapData.IsActive)
                {
                    return;
                }
            }

            EnemyData.StateEnemy = StateEnemy.Patrol;
            PatrolPointState += Modificator;
        }
    }
}
