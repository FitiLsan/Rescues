namespace Rescues
{
    public sealed class Services
    {
        #region Fields
        
        public static readonly Services SharedInstance = new Services();
        
        #endregion
        
        
        #region Properties
        
        public PhysicalServices PhysicalServices { get; private set; }
        public UnityTimeServices UnityTimeServices { get; private set; }
        public CameraServices CameraServices { get; private set; }
        public InputServices InputServices { get; private set; }
        
        #endregion
        
        
        #region Methods
        
        public void Initialize(Contexts contexts)
        {
            PhysicalServices = new PhysicalServices(contexts);
            UnityTimeServices = new UnityTimeServices(contexts);
            CameraServices = new CameraServices(contexts);
            InputServices = new InputServices(contexts);
        }
        
        #endregion
    }
}
