using UnityEngine;


namespace Rescues
{
    public sealed class InputController : IExecuteController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public InputController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            Vector2 inputAxis;
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");

            if (inputAxis.x != 0 || inputAxis.y != 0)
            {
                _context.Character.Move(inputAxis);
            }

            if (Input.GetButtonUp("Vertical"))
            {
                var interactableObject = GetInteractableObject<DoorTeleporterBehaviour>(InteractableObjectType.Door);
                _context.Character.Teleport(interactableObject.ExitPoint.position);
            }

            if (Input.GetButtonUp("PickUp"))
            {
                var interactableObject = GetInteractableObject<ItemBehaviour>(InteractableObjectType.Item);
                _context.Inventory.AddItem(interactableObject._itemData);
                Object.Destroy(interactableObject.GameObject);
            }
        }

        #endregion


        #region Methods

        private T GetInteractableObject<T>(InteractableObjectType type) where T : class
        {
            var interactableObjects = _context.GetTriggers(type);
            T behaviour = default;

            foreach (var trigger in interactableObjects)
            {
                if (trigger.IsInteractable)
                {
                    behaviour = trigger as T;
                }
            }

            return behaviour;
        }

        #endregion
    }
}
