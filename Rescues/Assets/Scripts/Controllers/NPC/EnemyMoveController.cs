using UnityEngine;


namespace Rescues
{
    public sealed class EnemyMoveController : IExecuteController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public EnemyMoveController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            //TODO bring all it method to EnemyBehavoiur into method Move()
            var enemy = _context.Enemy;
            var wayPointInfo = enemy.RouteData.GetWayPoints();

            if (Vector3.Distance(enemy.transform.position, wayPointInfo[enemy.PatrolPointState].PointPosition) > 0.5)
            {
                Vector3 movementDirection = Vector3.zero;
                movementDirection.x = wayPointInfo[enemy.PatrolPointState].PointPosition.x - enemy.transform.position.x;

                _context.Enemy.transform.position += movementDirection.normalized * enemy.EnemyData.Speed * Time.deltaTime;
                _context.Enemy.SetVisionDirection(movementDirection);
            }
            else
            {
                if(enemy.PatrolPointState +1 > wayPointInfo.Length - 1 || enemy.PatrolPointState -1 < 0)
                {
                    enemy.InvertModificator();
                }
                if(enemy.PatrolPointState + enemy.Modificator < 0)
                {
                    enemy.InvertModificator();
                }

                CheckWaitTime(enemy, wayPointInfo[enemy.PatrolPointState]);
            }
        }

        #endregion


        #region Methods

        private void CheckWaitTime(EnemyBehaviour enemy, WayPointInfo wayPointInfo)
        {
            if (enemy.CheckWaitTime(wayPointInfo.WaitTime)) return;
            else GetNewWayPoint(enemy);
        }

        private void GetNewWayPoint(EnemyBehaviour enemy)
        {
            enemy.PatrolPointState += enemy.Modificator;
        }

        #endregion
    }
}