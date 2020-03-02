using UnityEngine;


namespace Rescues
{
    public sealed class EnemyMoveController : IExecuteController
    {
        #region Fields

        public float waitingTime = 5f;
        public float maxDistance;
        public int detectionDistance;
        private readonly GameContext _context;
        public Vector3 Direction;
        public SpriteRenderer MySprite;

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
            if ((enemy.transform.position - _context.WayPoints[enemy.PatrolState]).sqrMagnitude < maxDistance)
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
            maxDistance = 0;
            yield return new WaitForSeconds(waitingTime);
            if(waitingTime <= 5)
            {
                maxDistance = 10;//примерное значение
            }
        }


        public void Detection()
        {
            RaycastHit2D hit = Physics2D.Raycast(Transform.position, Direction,_detectionDistance);
            if(hit != false)
            {
                Debug.Log(Defeat);
            }
        }


        #endregion
    }
}