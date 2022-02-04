using UnityEngine;

namespace Rescues
{
    public class MovableCameraState : CameraState
    {
        #region Fields

        private const float ACCELERATION_COEFFICIENT = 0.1f;

        private readonly GameContext _context;
        private readonly CameraServices _cameraServices;

        private float _deadZone;
        private float _cameraAccelerateStep;
        private float _cameraAcceleration;
        private float _targetPositionX;
        private float _targetPositionY;

        #endregion


        #region ClassLifeCycles

        public MovableCameraState(GameContext context, Services services)
        {
            _context = context;
            _cameraServices = services.CameraServices;
        }

        #endregion


        #region Methods

        public override void InitState(CameraData activeCamera)
        {
            _activeCamera = activeCamera;

            _cameraAccelerateStep = _activeCamera.CameraAccelerateStep * ACCELERATION_COEFFICIENT;
            _cameraAcceleration = 1f;
            _deadZone = _activeCamera.DeadZone;

            _targetPositionX = _context.character.Transform.position.x + _activeCamera.Position_X_Offset;
            var x = Mathf.Clamp(_targetPositionX, _activeCamera.MoveLeftXLimit, _activeCamera.MoveRightXLimit);
            _cameraServices.CameraMain.transform.position = new Vector3(x, _activeCamera.Position_Y_Offset,
                _cameraServices.CameraDepthConst);
        }

        public override void UpdateState()
        {
            if (_cameraServices.IsCameraFocused && _context.character.IsMoving)
                _cameraServices.ResetFocus();

            if (_cameraServices.IsCameraFocused)
                MoveCameraToTarget();
            else
                MoveCameraToCharacter();
        }

        private void MoveCameraToCharacter()
        {
            _targetPositionX = _context.character.Transform.position.x + _activeCamera.Position_X_Offset;
            _targetPositionY = _activeCamera.Position_Y_Offset;

            if (_context.character.IsMoving == false)
            {
                _deadZone = _activeCamera.DeadZone;
            }

            MoveCamera();
        }



        private void MoveCameraToTarget()
        {
            _targetPositionX = _cameraServices.OverridePosition.x + _activeCamera.Position_X_Offset;
            _targetPositionY = _cameraServices.OverridePosition.y;

            MoveCamera();
        }

        private void MoveCamera()
        {
            var cameraPositionX = _cameraServices.CameraMain.transform.position.x;
            var cameraPositionY = _cameraServices.CameraMain.transform.position.y;

            if (Mathf.Abs(_cameraServices.CameraMain.transform.position.x - _targetPositionX) > _deadZone ||
                !Mathf.Approximately(cameraPositionY, _activeCamera.Position_Y_Offset))
            {
                _cameraAcceleration = _cameraAccelerateStep * Time.deltaTime;
                _deadZone = 0;
                _cameraAcceleration = Mathf.Clamp(_cameraAcceleration, 0f, 1f);
                cameraPositionX = Mathf.Lerp(cameraPositionX, _targetPositionX, _cameraAcceleration);
                cameraPositionY = Mathf.Lerp(cameraPositionY, _targetPositionY, _cameraAcceleration);
            }

            cameraPositionX = Mathf.Clamp(cameraPositionX, _activeCamera.MoveLeftXLimit, _activeCamera.MoveRightXLimit);
            cameraPositionY = Mathf.Clamp(cameraPositionY, _activeCamera.MoveDownYLimit, _activeCamera.MoveUpYLimit);

            _cameraServices.CameraMain.transform.position = new Vector3(cameraPositionX, cameraPositionY,
                _cameraServices.CameraDepthConst);
        }

        #endregion
    }
}