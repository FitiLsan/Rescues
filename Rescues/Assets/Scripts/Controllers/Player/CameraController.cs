using UnityEngine;


namespace Rescues
{
    public sealed class CameraController : IExecuteController, IInitializeController, ITearDownController
    {
        #region Fields

        private readonly GameContext _context;
        private readonly CameraServices _cameraServices;
        private CameraData _activeCamera;
        private int _activeLocationHash;

        private CameraState _currentState;
        private CameraState _staticCameratState;
        private CameraState _movableCameraState;

        #endregion


        #region ClassLifeCycles

        public CameraController(GameContext context, Services services)
        {
            _context = context;
            _cameraServices = services.CameraServices;

            _staticCameratState = new StaticCameraState(context, services);
            _movableCameraState = new MovableCameraState(context, services);
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            if (_activeLocationHash != _context.activeLocation.GetHashCode())
            {
                SetCamera();
            }

            if (_currentState != null)
                _currentState.UpdateState();

            if (_cameraServices.IsCameraFree)
            {
                _cameraServices.MoveCameraWithMouse();
            }
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            var camTriggers = _context.GetTriggers(InteractableObjectType.CameraTrigger);
            foreach (var trigger in camTriggers)
            {
                var behavior = trigger as InteractableObjectBehavior;
                behavior.OnFilterHandler += OnFilterHandler;
                behavior.OnTriggerEnterHandler += OnTriggerEnterHandler;
                behavior.OnTriggerExitHandler += OnTriggerExitHandler;

                var cameraTrigger = trigger as CameraTrigger;
                cameraTrigger.Init(_cameraServices);
            }
        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            var camTriggers = _context.GetTriggers(InteractableObjectType.CameraTrigger);
            foreach (var trigger in camTriggers)
            {
                var behavior = trigger as InteractableObjectBehavior;
                behavior.OnFilterHandler -= OnFilterHandler;
                behavior.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                behavior.OnTriggerExitHandler -= OnTriggerExitHandler;
            }
        }

        #endregion


        #region Methods

        private void SetCamera()
        {
            _activeCamera = _context.activeLocation.CameraData;

            switch (_activeCamera.CameraMode)
            {
                case CameraMode.None:
                    return;

                case CameraMode.Moveable:
                    PresetMovableCamera();
                    break;

                case CameraMode.Static:
                    PlaceCameraOnLocation();
                    break;
            }

            _cameraServices.CameraMain.orthographicSize = _activeCamera.CameraSize;
            _cameraServices.CameraMain.backgroundColor = _context.activeLocation.BackgroundColor;
            _activeLocationHash = _context.activeLocation.GetHashCode();
        }

        private void PlaceCameraOnLocation()
        {
            _currentState = _staticCameratState;
            _currentState.InitState(_activeCamera);
        }

        private void PresetMovableCamera()
        {
            _currentState = _movableCameraState;
            _currentState.InitState(_activeCamera);
        }

         private bool OnFilterHandler(Collider2D obj)
        {
            return obj.CompareTag(TagManager.PLAYER);
        }

        private void OnTriggerEnterHandler(ITrigger enteredObject)
        {
            enteredObject.IsInteractable = true;
        }

        private void OnTriggerExitHandler(ITrigger enteredObject)
        {
            enteredObject.IsInteractable = false;
        }

        #endregion
    }
}
