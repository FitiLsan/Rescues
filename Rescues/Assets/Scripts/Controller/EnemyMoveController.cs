using UnityEngine;


namespace Rescues
{
    public sealed class EnemyMoveController : IExecuteController
    {
        #region Fields

        private readonly GameContext _context;

        public Vector3 _direction;
        public SpriteRenderer _mySprite;

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
            if (Vector3.Distance(enemy.transform.position, _context.WayPoints[enemy.PatrolState]) > 0.5)
            {
                Vector3 movementDirection = Vector3.zero;
                movementDirection.x = enemy.transform.position.x - _context.WayPoints[enemy.PatrolState].x;

                _context.Enemy.transform.position += movementDirection * enemy.EnemyData.Speed * Time.deltaTime;
            }
            else
            {
                if(enemy.PatrolState +1 > _context.WayPoints.Length - 1 || enemy.PatrolState -1 < 0)
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
            _context.Enemy.transform.Translate(_direction.x, 0, 0);   
            if (_direction.x != 0)
            {
                if (_direction.x > 0)
                {
                    _mySprite.flipX = !_mySprite.flipX;
                }
                else if (_direction.x < 0)
                {
                    _mySprite.flipX = !_mySprite.flipX;
                }
            }
        }

        #endregion
    }
}