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
            var enemyPositions = enemy.RouteData.GetWayPoints();

            if (Vector3.Distance(enemy.transform.position, enemyPositions[enemy.PatrolPointState]) > 0.5)
            {
                Vector3 movementDirection = Vector3.zero;
                movementDirection.x = enemyPositions[enemy.PatrolPointState].x - enemy.transform.position.x;

                _context.Enemy.transform.position += movementDirection.normalized * enemy.EnemyData.Speed * Time.deltaTime;
                _context.Enemy.SetVisionDirection(movementDirection);
            }
            else
            {
                if(enemy.PatrolPointState +1 > enemyPositions.Length - 1 || enemy.PatrolPointState -1 < 0)
                {
                    enemy.InvertModificator();
                }
                if(enemy.PatrolPointState + enemy.Modificator < 0)
                {
                    enemy.InvertModificator();
                }
                enemy.PatrolPointState += enemy.Modificator;
            }
        }

        #endregion


        #region Methods



        #endregion
    }
}