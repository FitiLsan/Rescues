using UnityEngine;


namespace Rescues
{
    public sealed class InputController : IExecuteController
    {
        private readonly GameContext _context;
        
        public InputController(GameContext context, Services services)
        {
            _context = context;
        }
        
        public void Execute()
        {
            _context.Character.Move(Input.GetAxis("Horizontal"));

            if (Input.GetKeyDown(KeyCode.R))
            {
                
            }
        }
    }
}
