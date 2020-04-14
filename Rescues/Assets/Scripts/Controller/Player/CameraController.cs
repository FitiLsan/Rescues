namespace Rescues
{
    public sealed class CameraController : IExecuteController
    {
        #region Fields
        
        private readonly GameContext _context;
        private readonly CameraServices _cameraServices;

        #endregion


        #region ClassLifeCycles
        
        public CameraController(GameContext context, Services services)
        {
            _context = context;
            _cameraServices = services.CameraServices;
        }

        #endregion
        
        
        public void Execute()
        {
//            _cameraServices.CameraMain
//            _context.Character.Transform.position
        }
    }
}
