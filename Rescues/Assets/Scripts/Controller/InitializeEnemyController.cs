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
            var enemyData = resources.EnemyData;
            var wayPoint = _context.WayPoints[0];

            var enemyObject = Object.Instantiate(resources, wayPoint, Quaternion.identity);
            _context.Enemy = enemyObject;
        }

        #endregion
    }
}
