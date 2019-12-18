namespace Rescues
{
    public sealed class MainControllers : Controllers
    {
        #region ClassLifeCycles
        
        public MainControllers(GameContext context, Services services)
        {
            Add(new InitializeCharacterController(context, services));
            Add(new InitializeInteractableObjectController(context, services));
            Add(new InitializeInventoryController(context, services));
            Add(new ItemPutInInventoryController(context, services));
            Add(new DoorTeleporterController(context, services));
            Add(new InputController(context, services));
        }

        #endregion
    }
}
