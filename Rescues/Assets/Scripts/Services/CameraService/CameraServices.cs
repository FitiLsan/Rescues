using UnityEngine;


namespace Rescues
{
    public sealed class CameraServices : Service
    {
        #region Fields

        private int _deltaTimeResetFrame;

        #endregion


        #region ClassLifeCycles

        public CameraServices(Contexts contexts) : base(contexts)
        {
            CameraMain = Camera.main;
        }

        #endregion
        
        
        #region Properties

        public Camera CameraMain;

        #endregion
    }
}
