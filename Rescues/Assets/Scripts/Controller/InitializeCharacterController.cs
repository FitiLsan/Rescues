using UnityEngine;


namespace Rescues
{
    public sealed class InitializeCharacterController : IInitializeController
    {
        private readonly GameContext _context;

        public InitializeCharacterController(GameContext context)
        {
            _context = context;
        }
        public void Initialize()
        {
            var resources = Resources.Load<PlayerBehaviour>(AssetsPathGameObject.Object[GameObjectType.Character]);
            var playerData = Data.PlayerData;
            var obj = Object.Instantiate(resources, playerData.Position, Quaternion.identity).transform;
            
            Character character = new Character(obj, playerData);
           
            _context.Character = character;
        }
    }
}
