using UnityEngine;


namespace Rescues
{
    public sealed class InitializeDoorTeleporterController : IInitializeController
    {
        #region Fields
        
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles
        
        public InitializeDoorTeleporterController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController
        
        public void Initialize()
        {
            var triggers = Object.FindObjectsOfType<DoorTeleporterBehaviour>();

            foreach (var trigger in triggers)
            {
                trigger.OnFilterHandler = OnFilterHandler;
                _context.AddTriggers(InteractableObjectType.Door, trigger);
            }
        }

        #endregion


        #region Methods
        
        private bool OnFilterHandler(Collider2D obj)
        {
            return obj.CompareTag(TagManager.PLAYER);
        }

        #endregion
    }
}
