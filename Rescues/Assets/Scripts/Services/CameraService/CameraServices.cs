using UnityEngine;


namespace Rescues
{
    public sealed class CameraServices : Service
    {
        #region Fields

        private const int CAMERA_DEPTH = -45;
        public Camera CameraMain;
        public CameraMode CameraMode;
        public bool IsCameraFree = false;
        
        private Vector3 _mouseOriginalClickPosition;
        private Vector3 _moveLimit;
        private float _cameraFreeMoveLimit;
        private int _cameraDragSpeed;

        #endregion


        #region ClassLifeCycles

        public CameraServices(Contexts contexts) : base(contexts)
        {
            CameraMain = Camera.main;           
            _cameraDragSpeed = Data.CameraData.CameraDragSpeed;
            _cameraFreeMoveLimit = Data.CameraData.CameraFreeMoveLimit;
        }

        #endregion


        #region Properties

        public int CameraDepthConst => CAMERA_DEPTH;

        #endregion


        #region Methods

        public void FreeCamera()
        {
            IsCameraFree = true;
            _mouseOriginalClickPosition = Input.mousePosition;           
            return;
        }

        public void FreeCameraMovement()
        {          
            Vector3 direction = CameraMain.ScreenToViewportPoint(Input.mousePosition - _mouseOriginalClickPosition);
            Vector3 move = new Vector3(direction.x * _cameraDragSpeed, direction.y * _cameraDragSpeed);
            _moveLimit = new Vector3(Mathf.Clamp(move.x, -_cameraFreeMoveLimit, _cameraFreeMoveLimit), Mathf.Clamp(move.y, -_cameraFreeMoveLimit, _cameraFreeMoveLimit));          
        }

        public void MoveCameraWithMouse()
        {
            CameraMain.transform.Translate(_moveLimit, Space.World);
        }

        public void LockCamera()
        {
            IsCameraFree = false;
        }    

        public void SetCamera(LocationData bootLocation)
        {
            CameraMode = bootLocation.CameraMode;
        }
        
        #endregion
    }
}
