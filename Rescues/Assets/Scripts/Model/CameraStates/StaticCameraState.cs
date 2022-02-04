using UnityEngine;

namespace Rescues
{
    public class StaticCameraState : CameraState
    {
        #region Fields

        private readonly GameContext _context;
        private readonly CameraServices _cameraServices;

        #endregion


        #region ClassLifeCycles

        public StaticCameraState(GameContext context, Services services)
        {
            _context = context;
            _cameraServices = services.CameraServices;
        }

        #endregion


        #region Methods

        public override void InitState(CameraData activeCamera)
        {
            var position = _context.activeLocation.LocationInstance.CameraPosition;
            _cameraServices.CameraMain.transform.position = new Vector3(position.x, position.y,
                _cameraServices.CameraDepthConst);
        }

        public override void UpdateState(){}

        #endregion
    }
}