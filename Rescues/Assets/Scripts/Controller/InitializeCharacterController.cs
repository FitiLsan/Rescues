using UnityEngine;


namespace Rescues
{
    public sealed class InitializeCharacterController : IInitializeController
    {
        public InitializeCharacterController(GameContext context)
        {
            
        }
        public void Initialize()
        { 
            var resources = Resources.Load(AssetsPathGameObject.Object[GameObjectType.Character]);
            // Load SO
            //var obj = Object.Instantiate(resources, SO.position, Quaternion.identity);
            
           // Character character = new Character(obj, SO);
           
           //context.Character = character;
        }
    }
}
