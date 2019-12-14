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
            
            if(Input.GetButtonUp("Vertical"))
            {
                var doors = _context.GetTriggers<DoorTeleporterBehaviour>(InteractableObjectType.Door);
                foreach (var trigger in doors)
                {
                    trigger.IsInteractable.CompareTo(true);

                    if(trigger.IsInteractable == true)
                    {
                        _context.Character.Teleport(trigger.ExitPoint.position);
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
