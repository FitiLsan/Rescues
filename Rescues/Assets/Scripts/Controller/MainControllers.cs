namespace Rescues
{
    public sealed class MainControllers : Controllers
    {
        public MainControllers(GameContext context)
        {
            Add(new InitializeCharacterController(context));
            Add(new InputController(context));
        }
    }
}
