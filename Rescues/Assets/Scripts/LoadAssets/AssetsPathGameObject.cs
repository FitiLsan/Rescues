using System.Collections.Generic;


namespace Rescues
{
    public sealed class AssetsPathGameObject
    {
        #region Fields

        public static readonly Dictionary<GameObjectType, string> Object = new Dictionary<GameObjectType, string>()
        {
            { GameObjectType.Character, "Prefabs/Player/Prefabs_Player_Character" },
<<<<<<< Updated upstream
            { GameObjectType.Enemy, "Prefabs/Enemy/Prefabs_Enemy_Patrolling" },
=======
            { GameObjectType.Enemy, "Prefabs/Enemies/Prefabs_Enemies_Patrolling" },
>>>>>>> Stashed changes
        };

        #endregion
    }
}
