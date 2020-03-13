using UnityEngine;


namespace Rescues
{
    public sealed class EnemyMoveController : IExecuteController
    {
        #region Fields

        public float WaitTime = 5.0f;
        public bool EndOfWait = false;
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
            var enemy = _context.Enemy;
            if ((enemy.transform.position - _context.WayPoints[enemy.PatrolState]).sqrMagnitude < enemy.MaxDistance * enemy.MaxDistance && !EndOfWait)
            {
                Wait();
                Vector3 movementDirection = Vector3.zero;
                movementDirection.x = enemy.transform.position.x - _context.WayPoints[enemy.PatrolState].x;

                _context.Enemy.transform.position += movementDirection * enemy.EnemyData.Speed * Time.deltaTime;
            }
            else
            {
                EndOfWait = false;
                if (enemy.PatrolState +1 > _context.WayPoints.Length - 1 || enemy.PatrolState -1 < 0)
                {

                    enemy.InvertModificator();
                }
                enemy.PatrolState += enemy.Modificator;
            }
        }

        #endregion


        #region Methods

        public void Flip()
        {
            var enemy = _context.Enemy;
            _context.Enemy.transform.Translate(enemy.Direction.x, 0, 0);   
            if (enemy.Direction.x != 0)
            {
                if (enemy.Direction.x > 0)
                {
                    enemy.MySprite.flipX = !enemy.MySprite.flipX;
                }
                else if (enemy.Direction.x < 0)
                {
                    enemy.MySprite.flipX = !enemy.MySprite.flipX;
                }
            }
        }


        public void Wait()
        {
            var enemy = _context.Enemy;
            enemy.MaxDistance = 0;
            EndOfWait = false;
            if(enemy.CurrenTime <= 0)
            {
                enemy.MaxDistance = 10.0f;
                enemy.CurrenTime = WaitTime;
                EndOfWait = true;
            }

            enemy.CurrenTime -= Time.deltaTime;
        }

        #endregion
    }
}