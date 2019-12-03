namespace Rescues
{
    public sealed class Services : Contexts
    {
        #region Fields
        
        public static readonly Services SharedInstance = new Services();
        
        #endregion
        
        
        #region Properties
        
        public PhysicsService PhysicsService { get; private set; }
        public UnityTimeService UnityTimeService { get; private set; }
        
        #endregion
        
        
        #region Methods
        
        public void Initialize(Contexts contexts)
        {
            PhysicsService = new PhysicsService(contexts);
            UnityTimeService = new UnityTimeService(contexts);
        }
        
        #endregion
    }
}
