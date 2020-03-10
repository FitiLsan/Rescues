using UnityEngine;


namespace Rescues
{
    public sealed class CameraServices : Service
    {
        #region Fields
               
        private Vector3 _origin;
        private float _cameraFreeMoveLimit = 50f;
        private int _cameraDragSpeed = 50;

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


        #region Methods

        public void CalculateOrigin()
        {           
            _origin = Input.mousePosition;
            return;
        }
        public void FreeCamera()
        {
            CustomDebug.Log(GetHashCode());           
            Vector3 direction = CameraMain.ScreenToViewportPoint(Input.mousePosition - _origin);
            Vector3 move = new Vector3(direction.x * _cameraDragSpeed, direction.y * _cameraDragSpeed);
            Vector3 movelimit = new Vector3(Mathf.Clamp(move.x, -_cameraFreeMoveLimit, _cameraFreeMoveLimit), Mathf.Clamp(move.y, -_cameraFreeMoveLimit, _cameraFreeMoveLimit));
            CameraMain.transform.Translate(movelimit, Space.World);
        }

        #endregion
    }
}
