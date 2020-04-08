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
                _context.Character.StateMoving(inputAxis);
            }

            if (Input.GetButtonUp("Vertical"))
            {
                var interactableObject = GetInteractableObject<DoorTeleporterBehaviour>(InteractableObjectType.Door);
                if (interactableObject != null)
                {
                    _context.Character.StateTeleporting(interactableObject.ExitPoint.position);
                }
            }

            if (Input.GetButtonUp("PickUp"))
            {
                var interactableObject = GetInteractableObject<ItemBehaviour>(InteractableObjectType.Item);
                if (interactableObject != null)
                {
                    if (_context.Inventory.AddItem(interactableObject._itemData))
                    {
                        Object.Destroy(interactableObject.GameObject);
                    }
                }
            }

            _context.Character.StateHandler();

            if (Input.GetButtonUp("Use"))
            {
                var interactableObject = GetInteractableObject<HidingPlaceBehaviour>(InteractableObjectType.HidingPlace);
                if (_context.Character.PlayerState == State.Hiding)
                {
                    _context.Character.StateHideAnimation(interactableObject);                   
                }
                if (interactableObject != null)
                {
                    _context.Character.StateHideAnimation(interactableObject);
                }
            }
            _context.Character.AnimationPlay.UpdateTimer();

            if (_context.Character.AnimationPlay.IsEvent())
            {
                _context.Character.StateHiding();
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
                    break;
                }
            }

            return behaviour;
        }

        #endregion
    }
}
