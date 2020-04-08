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
                if (interactableObject != null)
                {
                    _context.Character.Teleport(interactableObject.ExitPoint.position);
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

            if (Input.GetButtonUp("Use"))
            {
                var interactableObject = GetInteractableObject<HidingPlaceBehaviour>(InteractableObjectType.HidingPlace);
                if (_context.Character.IsColliderOn == false)
                {
                    _context.Character.PlayAnimationWithTimer();
                    _context.Character.IsHiding = true;
                }                             
                if (interactableObject != null)
                {
                    _context.Character.StartHiding(interactableObject);
                }
            }
            _context.Character.AnimationPlay.UpdateTimer();  
            if(_context.Character.AnimationPlay.IsEvent() && _context.Character.IsHiding == true)
            {
                _context.Character.IsPlayingAnimation = false;
                _context.Character.Hiding();
                _context.Character.IsHiding = false;
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
