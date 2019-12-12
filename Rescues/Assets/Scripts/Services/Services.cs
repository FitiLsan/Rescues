namespace Rescues
{
    public sealed class Services
    {
        #region Fields
        
        public static readonly Services SharedInstance = new Services();
        
        #endregion
        
        
        #region Properties
        
        public PhysicsService PhysicsService { get; private set; }
        public UnityTimeService UnityTimeService { get; private set; }
        public CameraServices CameraServices { get; private set; }
        
        #endregion
        
        
        #region Methods
        
        public void Initialize(Contexts contexts)
        {
            PhysicsService = new PhysicsService(contexts);
            UnityTimeService = new UnityTimeService(contexts);
            CameraServices = new CameraServices(contexts);
        }
        
        #endregion
    }
}
