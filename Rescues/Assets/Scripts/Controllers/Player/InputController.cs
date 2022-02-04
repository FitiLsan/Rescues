using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VIDE_Data;


namespace Rescues
{
    public sealed class InputController : IExecuteController
    {
        #region Fields

        public EventSystem eventSystem;

        private readonly GameContext _context;
        private readonly CameraServices _cameraServices;
        private readonly PhysicalServices _physicsService;
        private readonly InputServices _inputService;
        private PlayerStates _lastState;
        private Action _cancelState = () => { };
        private Vector2 _inputAxis;
        private bool _isStateLocked;

        #endregion


        #region ClassLifeCycles

        public InputController(GameContext context, Services services)
        {
            _context = context;
            _cameraServices = services.CameraServices;
            _physicsService = services.PhysicalServices;
            _inputService = services.InputServices;
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            if (_inputService.CancelButton.IsUp)
            {
                if (_lastState != PlayerStates.Idle && _lastState != PlayerStates.Moving)
                {
                    _isStateLocked = false;
                    SwitchState(PlayerStates.Idle);
                }
                else
                {
                    _context.gameMenu.SwitchState();
                }
            }

            _inputAxis.x = _inputService.HorizontalAxis.Value;
            _inputAxis.y = _inputService.VerticalAxis.Value;

            if (_physicsService.IsPaused == false && _isStateLocked == false)
            {
                if (_inputAxis.x != 0)
                {
                    _context.character.Move(_inputAxis.x);
                    _lastState = PlayerStates.Moving;
                    _cancelState.Invoke();
                    _cancelState = () => { };
                }
                else
                {
                    _context.character.SetIdle();
                    if (_inputService.VerticalAxis.IsReleased)
                    {
                        SwitchState(PlayerStates.GoByGateWay);
                    }


                    if (_inputService.PickUpButton.IsUp)
                    {
                        SwitchState(PlayerStates.PickUp);
                    }

                    if (_inputService.InventoryButton.IsUp)
                    {
                        SwitchState(PlayerStates.Inventory);
                    }

                    if (_inputService.NotepadButton.IsUp)
                    {
                        SwitchState(PlayerStates.Notepad);
                    }

                    if (_inputService.UseButton.IsUp)
                    {
                        SwitchState(PlayerStates.Use);
                    }

                    if (_inputService.MouseScrollButton.IsDown)
                    {
                        _cameraServices.FreeCamera();
                    }

                    if (_inputService.MouseScrollButton.IsHeld)
                    {
                        _cameraServices.FreeCameraMovement();
                    }

                    if (_inputService.MouseScrollButton.IsUp)
                    {
                        _cameraServices.LockCamera();
                    }
                }
            }
        }

        #endregion


        #region Methods

        private void LockState()
        {
            _context.character.ForceSetIdle();
            _isStateLocked = true;
        }

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

        private List<T> GetInteractableObjects<T>(InteractableObjectType type) where T : class
        {
            var interactableObjects = _context.GetTriggers(type);
            List<T> behaviours = new List<T>();

            foreach (var trigger in interactableObjects)
            {
                if (trigger.IsInteractable)
                {
                    behaviours.Add(trigger as T);
                }
            }

            return behaviours;
        }

        private void SwitchState(PlayerStates playerState)
        {
            _cancelState.Invoke();

            switch (playerState)
            {
                case PlayerStates.Use:
                    {
                        _cancelState = () => { };

                        DialogueBehaviour dialogue = GetInteractableObject<DialogueBehaviour>(InteractableObjectType.Dialogue);
                        if (dialogue != null && !dialogue.IsInteractionLocked)
                        {
                            _context.dialogueUIController.Begin(dialogue.assignDialog);
                            VD.OnEnd += (data) => _isStateLocked = false;
                            _cancelState += () =>
                            {
                                _context.dialogueUIController.End(new VD.NodeData());
                                VD.OnEnd -= (data) => _isStateLocked = false;
                            };
                            LockState();
                        }

                        List<EventSystemBehaviour> eventSystems = GetInteractableObjects<EventSystemBehaviour>(InteractableObjectType.EventSystem);
                        if (eventSystems != null)
                        {
                            foreach (EventSystemBehaviour es in eventSystems)
                            {
                                if (!es.IsInteractionLocked)
                                {
                                    es.ActivateButtonInTriggerEvent();
                                }
                            }
                        }

                        ItemBehaviour item = GetInteractableObject<ItemBehaviour>(InteractableObjectType.Item);
                        if (item != null && !item.IsInteractionLocked)
                        {
                            LockState();
                            _context.character.SetPickUp(item.PickUpTime);
                            TimeRemainingExtensions.AddTimeRemaining(new TimeRemaining(() =>
                            {
                                item.gameObject.SetActive(false);
                                _context.inventory.AddItem(item.ItemData);
                                _isStateLocked = false;
                            },
                            item.PickUpTime));
                        }

                        PuzzleBehaviour puzzleObject = GetInteractableObject<PuzzleBehaviour>(InteractableObjectType.Puzzle);
                        if (puzzleObject != null && !puzzleObject.Puzzle.IsFinished && !puzzleObject.IsInteractionLocked)
                        {
                            puzzleObject.Puzzle.Activate();
                            LockState();
                            _cancelState += puzzleObject.Puzzle.Close;
                            _cancelState += () => _isStateLocked = false;
                            puzzleObject.Puzzle.Finished += (Puzzle) => _cancelState.Invoke();
                        }

                        CameraTrigger cameraTrigger = GetInteractableObject<CameraTrigger>(InteractableObjectType.CameraTrigger);
                        if (cameraTrigger != null)
                        {
                            int focusID = Time.frameCount;
                            _cameraServices.SetCameraFocusWithID(cameraTrigger.TargetPoint, focusID);

                            if (cameraTrigger.IsReturningOnTimer)
                            {
                                TimeRemainingExtensions.AddTimeRemaining(new TimeRemaining(() =>
                                {
                                    _cameraServices.ResetFocusWithID(focusID);
                                },
                                cameraTrigger.FocusTime));
                            }
                        }
                    }
                    break;

                case PlayerStates.PickUp:
                    {
                        TrapBehaviour trap = GetInteractableObject<TrapBehaviour>(InteractableObjectType.Trap);
                        if (trap != null && !trap.IsInteractionLocked)
                        {
                            if (_context.inventory.Contains(trap.TrapInfo.RequiredTrapItem))
                            {
                                LockState();
                                //TODO Need animation for this
                                TimeRemainingExtensions.AddTimeRemaining(new TimeRemaining(() =>
                                {
                                    trap.CreateTrap();
                                    _context.inventory.RemoveItem(trap.TrapInfo.RequiredTrapItem);
                                    _isStateLocked = false;
                                },
                                trap.TrapInfo.BaseTrapData.CraftingTime));
                            }
                        }

                        _cancelState = () => { };
                    }
                    break;

                case PlayerStates.GoByGateWay:
                    {
                        Gate gate = GetInteractableObject<Gate>(InteractableObjectType.Gate);
                        if (gate != null && !gate.IsInteractionLocked)
                        {
                            LockState();
                            gate.GoByGateWay(() => _isStateLocked = false);
                        }

                        _cancelState = () => { };
                    }
                    break;
            }

            #region CloseOnSecondClick

            if (playerState != _lastState)
            {
                _lastState = playerState;
                switch (playerState)
                {
                    case PlayerStates.Use:
                        {
                            //TODO not implemented
                            HidingPlaceBehaviour hidingPlace = GetInteractableObject<HidingPlaceBehaviour>(InteractableObjectType.HidingPlace);
                            if (hidingPlace != null && !hidingPlace.IsInteractionLocked)
                            {
                                _context.character.StartHiding(hidingPlace);
                            }

                            //TODO not working yet
                            StandBehaviour stand = GetInteractableObject<StandBehaviour>(InteractableObjectType.Stand);
                            if (stand != null && !stand.IsInteractionLocked)
                            {
                                stand.StandWindow.SetActive(true);
                                _cancelState += () =>
                                {
                                    stand.StandWindow.SetActive(false);
                                };
                            }
                        }
                        break;

                    case PlayerStates.Inventory:
                        {
                            _context.inventory.gameObject.SetActive(true);

                            _cancelState = () =>
                            {
                                _context.inventory.gameObject.SetActive(false);
                            };
                        }
                        break;

                    case PlayerStates.Notepad:
                        {
                            _context.notepad.gameObject.SetActive(true);

                            _cancelState = () =>
                            {
                                _context.notepad.gameObject.SetActive(false);
                            };
                        }
                        break;
                }
            }
            else
            {
                _lastState = PlayerStates.Idle;
            }

            #endregion
        }

        #endregion
    }
}

