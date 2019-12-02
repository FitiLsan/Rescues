using UnityEngine;


namespace Rescues
{
    public sealed class InputController :  IExecuteController
    {
        Vector3 dir = Vector3.zero;
        public void Execute()
        {
            dir.x = Input.GetAxis("Horizontal");
            CustomDebug.Log(dir);
        }
    }
}
