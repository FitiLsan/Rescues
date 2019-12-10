using UnityEngine;


namespace Rescues
{
    public sealed class InputController : IExecuteController
    {
        #region Fields
        
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles
        
        public InputController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IExecuteController
        
        public void Execute()
        {
            _context.Character.Move(Input.GetAxis("Horizontal"));
            
            if(Input.GetAxis("Vertical") != 0)
            {
                var doors = _context.GetTriggers<DoorTeleporterBehaviour>(TriggerObjectType.Door);
                foreach (var trigger in doors)
                {
                    trigger.IsInteractable.CompareTo(true);

                    if(trigger.IsInteractable == true)
                    {
                        _context.Character.Teleport();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                
            }
        }

        #endregion
    }
}
