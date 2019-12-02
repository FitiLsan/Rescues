using UnityEngine;


namespace Rescues
{
    public sealed class InputController : IExecuteController
    {
        public InputController(GameContext context)
        {
            
        }
        
        
        Vector3 dir = Vector3.zero;
        public void Execute()
        {
            dir.x = Input.GetAxis("Horizontal");
            CustomDebug.Log(dir);
        }
    }
}
