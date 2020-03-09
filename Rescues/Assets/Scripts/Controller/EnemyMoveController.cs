using UnityEngine;


namespace Rescues
{
    public sealed class EnemyMoveController : IExecuteController
    {
        #region Fields

        public float waitTime = 5.0f;
        public float currenTime;
        public float MaxDistance;
        public int detectionDistance;
        public bool endOfWait = false;
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
            if ((enemy.transform.position - _context.WayPoints[enemy.PatrolState]).sqrMagnitude < MaxDistance * MaxDistance && !endOfWait)
            {
                Wait();
                Vector3 movementDirection = Vector3.zero;
                movementDirection.x = enemy.transform.position.x - _context.WayPoints[enemy.PatrolState].x;

                _context.Enemy.transform.position += movementDirection * enemy.EnemyData.Speed * Time.deltaTime;
            }
            else
            {
                endOfWait = false;
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
            _context.Enemy.transform.Translate(Direction.x, 0, 0);   
            if (Direction.x != 0)
            {
                if (Direction.x > 0)
                {
                    MySprite.flipX = !MySprite.flipX;
                }
                else if (Direction.x < 0)
                {
                    MySprite.flipX = !MySprite.flipX;
                }
            }
        }


        public void Wait()
        {
            MaxDistance = 0;
            endOfWait = false;
            if(currenTime <= 0)
            {
                MaxDistance = 10;//возможное значение
                currenTime = waitTime;
                endOfWait = true;
            }

            currenTime -= Time.deltaTime;
        }

        #endregion
    }
}