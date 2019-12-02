namespace Rescues
{
    public sealed class GameSystemsController : GameStateController
    {
        public GameSystemsController(GameContext context)
        {
            AddUpdateFeature(new MainControllers(context));
        }
    }
}
