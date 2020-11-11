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

            _context.Character.StateHandler();

            _context.Character.AnimationPlayTimer.UpdateTimer();

            //TODO сделать адекватное управление. Теперь перс просто перебирает точки Vector3 из своего пути, физики в движении нет
            if (inputAxis.x != 0)
            {
                var direction = inputAxis.x > 0 ? 1 : -1;
                _context.Character.StateMoving(direction);
            }
            else
            {
                _context.Character.StateMoving(0); 
            }

            if (Input.GetButtonUp("Vertical"))
            {
                var interactableObject = GetInteractableObject<Gate>(InteractableObjectType.Gate);
                if (interactableObject != null)
                {
                    _context.Character.StateTeleporting(interactableObject);
                }
            }

            if (Input.GetButtonUp("PickUp"))
            {
                var interactableObject = GetInteractableObject<ItemBehaviour>(InteractableObjectType.Item);
                if (interactableObject != null)
                {
                    _context.Character.StatePickUpAnimation(interactableObject);
                    Object.Destroy(interactableObject.GameObject);
                }

                var trapBehaviour = GetInteractableObject<TrapBehaviour>(InteractableObjectType.Trap);
                if (trapBehaviour != null)
                {
                    if (_context.Inventory.Contains(trapBehaviour.TrapInfo.RequiredTrapItem))
                    {
                        _context.Character.StateCraftTrapAnimation(trapBehaviour);
                    }
                }
            }

            if (Input.GetButtonUp("Inventory"))
            {
                _context.Inventory.gameObject.SetActive(!_context.Inventory.gameObject.activeSelf);
            }

            if (Input.GetButtonDown("Use"))
            {
                var puzzleObject = GetInteractableObject<PuzzleBehaviour>(InteractableObjectType.Puzzle);
                if (puzzleObject != null)
                {
                    puzzleObject.Puzzle.Activate();
                }
                
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

            if (_context.Character.AnimationPlayTimer.IsEvent())
            {
                switch (_context.Character.PlayerState)
                {
                    case State.HideAnimation:
                        {
                            _context.Character.StateHiding();
                            break;
                        }

                    case State.PickUpAnimation:
                        {
                            var item = _context.Character.InteractableItem as ItemBehaviour;
                            _context.Inventory.AddItem(item.ItemData);
                            _context.Character.StateIdle();
                            break;
                        }

                    case State.CraftTrapAnimation:
                        {
                            var trap = _context.Character.InteractableItem as TrapBehaviour;
                            trap.CreateTrap();
                            _context.Inventory.RemoveItem(trap.TrapInfo.RequiredTrapItem);
                            _context.Character.StateIdle();
                            break;
                        }
                    case State.GoByGateWay:
                        {
                            _context.Character.GoByGateWay();
                            _context.Character.StateIdle();
                            break;
                        }

                }
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
