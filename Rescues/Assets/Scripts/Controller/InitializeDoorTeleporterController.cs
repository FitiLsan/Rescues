using UnityEngine;


namespace Rescues
{
    public sealed class InitializeDoorTeleporterController : IInitializeController
    {
        private readonly GameContext _context;

        public InitializeDoorTeleporterController(GameContext context)
        {
            _context = context;
        }
        
        public void Initialize()
        {
            var triggers = Object.FindObjectsOfType<DoorTeleporterBehaviour>();

            foreach (var trigger in triggers)
            {
                trigger.OnFilterHandler = OnFilterHandler;
                _context.AddTriggers(TriggerObjectType.Door, trigger);
            }
        }

        private bool OnFilterHandler(Collider2D obj)
        {
           return obj.CompareTag("Player");
        }
    }
}
