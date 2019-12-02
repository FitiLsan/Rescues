namespace Rescues
{
    public sealed class GameSystemsController : GameStateController
    {
        public GameSystemsController()
        {
            AddUpdateFeature(new MainControllers());
        }
    }
}
