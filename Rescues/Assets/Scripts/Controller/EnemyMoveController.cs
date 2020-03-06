using UnityEngine;


namespace Rescues
{
    public sealed class EnemyMoveController : IExecuteController
    {
        #region Fields

        public float WaitingTime = 5.0f;
        public float MaxDistance;
        public int detectionDistance;
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
            if ((enemy.transform.position - _context.WayPoints[enemy.PatrolState]).sqrMagnitude < MaxDistance)
            {
                StartCoroutine(Wait());
                Vector3 movementDirection = Vector3.zero;
                movementDirection.x = enemy.transform.position.x - _context.WayPoints[enemy.PatrolState].x;

                _context.Enemy.transform.position += movementDirection * enemy.EnemyData.Speed * Time.deltaTime;
            }
            else
            {
                StartCoroutine(Wait());
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



        IEnumerator Wait()
        {
            MaxDistance = 0;
            yield return new WaitForSeconds(WaitingTime);
            if(WaitingTime <= 5)
            {
                MaxDistance = 10;//примерное значение
            }
        }

        #endregion
    }
}