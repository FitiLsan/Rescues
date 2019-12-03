namespace Rescues
{
    public sealed class GameSystemsController : GameStateController
    {
        public GameSystemsController(GameContext context, Services services)
        {
            AddUpdateFeature(new MainControllers(context, services));
        }
    }
}
