using UnityEngine;
using UnityEngine.EventSystems;


namespace Rescues
{
    public sealed class InputController : IExecuteController
    {
        #region Fields

        public EventSystem eventSystem;

        private readonly GameContext _context;
        private readonly CameraServices _cameraServices;
        private GameObject _interfaceWindow;

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
            //TODO не надо обрабатывать управление, пока локация не загружена, нужно что-то поулчше, чем проверка на НАЛЛ каждый вызов
            if (!_context.Character.CurentCurveWay) return;
            
            Vector2 inputAxis;
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");
           
            _context.Character.SetScale();
            _context.Character.StateHandler();
            _context.Character.AnimationPlayTimer.UpdateTimer();
            
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

            if (Input.GetButtonUp("Use"))
            {
                var puzzleObject = GetInteractableObject<PuzzleBehaviour>(InteractableObjectType.Puzzle);
                if (puzzleObject != null && !puzzleObject.Puzzle.IsFinished)
                {
                    puzzleObject.Puzzle.Activate();
                    Time.timeScale = 0;
                }

                var hidingPlace = GetInteractableObject<HidingPlaceBehaviour>(InteractableObjectType.HidingPlace);

                if (_context.Character.PlayerState == State.Hiding)
                {
                    _context.Character.StateHideAnimation(hidingPlace);
                }

                if (hidingPlace != null)
                {
                    _context.Character.StateHideAnimation(hidingPlace);
                }

                var stand = GetInteractableObject<StandBehaviour>(InteractableObjectType.Stand);
                if (stand != null)
                {
                    OpenInterfaceWindow(stand.StandWindow.gameObject, stand.StandWindow.GetComponent<StandUI>().StandItemSlots[0].gameObject);
                }
            }


            if (Input.GetMouseButtonUp(0))
            {
                if (_interfaceWindow != null)
                {
                    var item = _interfaceWindow.GetComponent<StandUI>();
                    if (item != null)
                    {
                        if (!item.IsItemOpened)
                        {
                            item.OpenStandItemWindow();
                        }
                        else if (item.Item != null && !_context.Inventory.Contains(item.Item) && item.IsMouseIn)
                        {
                            _context.Inventory.AddItem(item.Item);
                            item.StandItemSlots[item.SlotNumber].gameObject.SetActive(false);
                            item.StandItemSlots.RemoveAt(item.SlotNumber);
                        }
                        else if (item.Item == null)
                        {
                            item.PlayDontNeedItem();
                        }
                        if (!item.IsMouseIn && item.IsItemOpened)
                        {
                            item.CloseStandItemWindow();
                        }
                        else if (!item.IsMouseIn)
                        {
                            CloseInterfaceWindow();
                        }
                    }
                }
            }

            if (Input.GetButtonUp("Submit"))
            {
                if (_interfaceWindow != null)
                {
                    var item = _interfaceWindow.GetComponent<StandUI>();
                    if (item != null)
                    {
                        if (!item.IsItemOpened)
                        {
                            item.OpenStandItemWindow();
                        }
                        else if (item.Item != null && !_context.Inventory.Contains(item.Item))
                        {
                            _context.Inventory.AddItem(item.Item);
                            item.StandItemSlots[item.SlotNumber].gameObject.SetActive(false);
                            item.StandItemSlots.RemoveAt(item.SlotNumber);
                        }
                        else if (item.Item == null)
                        {
                            item.PlayDontNeedItem();
                        }
                    }
                }
            }

            if (Input.GetButtonUp("Cancel"))
            {
                if (_interfaceWindow != null)
                {
                    var item = _interfaceWindow.GetComponent<StandUI>();
                    if (item != null && item.IsItemOpened)
                    {
                        item.CloseStandItemWindow();
                        EventSystem.current.SetSelectedGameObject(item.StandItemSlots[item.SlotNumber].gameObject);
                    }
                    else if (item != null)
                    {
                        CloseInterfaceWindow();
                    }
                }

                var puzzleObject = GetInteractableObject<PuzzleBehaviour>(InteractableObjectType.Puzzle);
                if (puzzleObject != null)
                {
                    puzzleObject.Puzzle.Close();                   
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

        private void CloseInterfaceWindow()
        {
            _interfaceWindow.SetActive(false);
            Time.timeScale = 1f;
            _interfaceWindow = null;
            EventSystem.current.SetSelectedGameObject(null);
        }

        private void OpenInterfaceWindow(GameObject interfaceWindow, GameObject selectedObject = null)
        {
            _interfaceWindow = interfaceWindow;
            _interfaceWindow.SetActive(true);
            EventSystem.current.SetSelectedGameObject(selectedObject);
            Time.timeScale = 0f;
        }

        #endregion
    }
}

