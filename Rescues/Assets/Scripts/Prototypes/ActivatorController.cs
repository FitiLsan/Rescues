using UnityEngine;


namespace Rescues
{
    public class ActivatorController : IExecuteController, IInitializeController
    {
        private PrototypePlayerActivator _playerActivator;

        public void Initialize()
        {
            _playerActivator = Object.FindObjectOfType<PrototypePlayerActivator>();
        }

        public void Execute()
        {         
            if (Input.GetButtonUp("Use"))
            {               
                if (_playerActivator.CurrentTrigger != null)
                {
                    _playerActivator.CurrentTrigger.ActivateButtonInTriggerEvent();
                } 
            }
        }
    }
}
