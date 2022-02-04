namespace Rescues
{
    public sealed class MainControllers : Controllers
    {
        #region ClassLifeCycles

        public MainControllers(GameContext context, Services services)
        {
            Add(new LevelController(context, services));
            Add(new InitializeCharacterController(context, services));
            Add(new TimeRemainingController());
            Add(new ItemActiveController(context, services));
            Add(new MainPuzzleController(context, services));
            Add(new HidingPlaceController(context, services));
            Add(new EventSystemController(context));
            Add(new DialogueUIController(context));
            Add(new InputController(context, services));
            Add(new InventoryController(context));
            Add(new NotepadController(context));
            Add(new InteractionPromptController(context, services));
        }

        #endregion
    }
}
