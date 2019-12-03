namespace Rescues
{
    public sealed class Services : Contexts
    {
        public static Services sharedInstance = new Services();
        public PhysicsService PhysicsService { get; private set; }
        public UnityTimeService UnityTimeService { get; private set; }


        public void Initialize(Contexts contexts)
        {
            PhysicsService = new PhysicsService(contexts);
            UnityTimeService = new UnityTimeService(contexts);
        }
    }
}
