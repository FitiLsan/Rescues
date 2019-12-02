using UnityEngine;


namespace Rescues
{
    public sealed class InputController : IExecuteController
    {
        private readonly GameContext _context;
        
        public InputController(GameContext context)
        {
            _context = context;
        }
        
        public void Execute()
        {
            Vector3 dir = Vector3.zero;
            dir.x = Input.GetAxis("Horizontal");
            CustomDebug.Log(dir);
            //context.Character.Move(dir);
        }
    }
}
