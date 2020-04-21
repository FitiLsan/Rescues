using UnityEngine;


namespace Rescues
{
    public sealed class InitializeEnemyController : IInitializeController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public InitializeEnemyController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            var resources = Resources.Load<EnemyBehaviour>(AssetsPathGameObject.Object[GameObjectType.Enemy]);

            var enemyObject = Object.Instantiate(resources, Vector3.zero, Quaternion.identity);
            _context.Enemy = enemyObject;
            CustomDebug.Log(_context.Enemy);
            CustomDebug.Log(_context.Enemy.RouteData);
            CustomDebug.Log(_context.Enemy.RouteData.GetWayPoints());
            var wayPoint = _context.Enemy.RouteData.GetWayPoints()[0];
            enemyObject.transform.position = wayPoint;
        }

        #endregion
    }
}
