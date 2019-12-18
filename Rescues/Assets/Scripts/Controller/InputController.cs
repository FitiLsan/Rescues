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
            Vector2 inputAxis;
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");

            if (inputAxis.x != 0 || inputAxis.y != 0)
            {
                _context.Character.Move(inputAxis);
            }

            if(Input.GetButtonUp("Vertical"))
            {
                var doors = _context.GetTriggers(InteractableObjectType.Door);
                foreach (var trigger in doors)
                {
                    if (trigger.IsInteractable)
                    {
                        var doorTeleporterBehaviour = trigger as DoorTeleporterBehaviour;
                        _context.Character.Teleport(doorTeleporterBehaviour.ExitPoint.position);
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
