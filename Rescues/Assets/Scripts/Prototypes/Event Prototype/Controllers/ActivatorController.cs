using UnityEngine;


namespace Rescues
{
    public class ActivatorController : IExecuteController, IInitializeController
    {
        #region Fields

        private PlayerActivator _playerActivator;

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            _playerActivator = Object.FindObjectOfType<PlayerActivator>();
        }

        #endregion


        #region IExecuteController

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

        #endregion
    }
}
