using UnityEngine;


namespace Rescues
{
    public sealed class InputController : IExecuteController
    {
        #region Fields

        private readonly GameContext _context;
        private readonly CameraServices _cameraServices;       

        #endregion


        #region ClassLifeCycles

        public InputController(GameContext context, Services services)
        {
            _context = context;
            _cameraServices = services.CameraServices;
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

                var trapBehaviour = GetInteractableObject<TrapBehaviour>(InteractableObjectType.Trap);
                if (trapBehaviour != null)
                {
                    if (_context.Inventory.Contains(trapBehaviour.TrapInfo.RequiredTrapItem))
                    {
                        trapBehaviour.CreateTrap();
                        _context.Inventory.RemoveItem(trapBehaviour.TrapInfo.RequiredTrapItem);
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
          
            if (Input.GetButtonDown("Mouse ScrollPressed"))
            {
                _cameraServices.FreeCamera();              
            }

            if (Input.GetButton("Mouse ScrollPressed"))
            {
                _cameraServices.FreeCameraMovement();
            }

            if (Input.GetButtonUp("Mouse ScrollPressed"))
            {
                _cameraServices.LockCamera();               
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
