using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rescues
{
    public sealed class InteractionPromptController : IInitializeController, ITearDownController, IExecuteController
    {
        #region Fields

        private readonly GameContext _context;
        private readonly InputServices _inputService;

        private readonly Dictionary<InteractionPrompt, InputButton> _prompts = new Dictionary<InteractionPrompt, InputButton>();

        private Canvas _canvas;
        private Dictionary<InteractableObjectBehavior, InputPromptView> _activePrompts = new Dictionary<InteractableObjectBehavior, InputPromptView>();
        private Stack<InputPromptView> _availablePrompts = new Stack<InputPromptView>();

        private InputPromptsPrefabData _inputPromptsPrefabData;

        private InputDevice _activeDevice;
        private InputDevice _lastActiveDevice;
        private bool _wasDeviceChanged;

        #endregion


        #region ClassLifeCycles

        public InteractionPromptController(GameContext context, Services services)
        {
            _context = context;
            _inputService = services.InputServices;

            FillPrompts();

            _inputPromptsPrefabData = Resources.Load<InputPromptsPrefabData>(AssetsPathGameObject.InputData[InputDataType.InputPromptsPrefabData]);
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            var interactables = _context.GetListInteractable();
            foreach (var interactable in interactables)
            {
                if (!(interactable is InteractableObjectBehavior) ||
                    (interactable as InteractableObjectBehavior).InteractionPrompt == InteractionPrompt.None)
                    continue;

                var trigger = interactable as ITrigger;
                trigger.OnTriggerEnterHandler += OnTriggerEnterHandler;
                trigger.OnTriggerExitHandler += OnTriggerExitHandler;
            }
        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            var interactables = _context.GetListInteractable();
            foreach (var interactable in interactables)
            {
                var trigger = interactable as ITrigger;
                trigger.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                trigger.OnTriggerExitHandler -= OnTriggerExitHandler;
            }

            if (_canvas != null)
                Object.Destroy(_canvas);
        }

        #endregion


        # region IExecuteController

        public void Execute()
        {
            GetActiveDevice();

            if (_wasDeviceChanged)
                UpdatePrompts();
        }

        #endregion


        #region Methods

        private void FillPrompts()
        {
            _prompts.Add(InteractionPrompt.Use, _inputService.UseButton);
            _prompts.Add(InteractionPrompt.Inventory, _inputService.InventoryButton);
            _prompts.Add(InteractionPrompt.Notepad, _inputService.NotepadButton);
            _prompts.Add(InteractionPrompt.PickUp, _inputService.PickUpButton);
            _prompts.Add(InteractionPrompt.HorizontalAxisPositive, _inputService.HorizontalAxis.Positive);
            _prompts.Add(InteractionPrompt.HorizontalAxisNegative, _inputService.HorizontalAxis.Negative);
            _prompts.Add(InteractionPrompt.VerticalAxisPositive, _inputService.VerticalAxis.Positive);
            _prompts.Add(InteractionPrompt.VerticalAxisNegative, _inputService.VerticalAxis.Negative);
        }

        private void OnTriggerEnterHandler(ITrigger enteredObject)
        {
            var interactable = enteredObject as InteractableObjectBehavior;

            if (interactable != null)
                ShowPrompt(interactable);
        }

        private void OnTriggerExitHandler(ITrigger enteredObject)
        {
            var interactable = enteredObject as InteractableObjectBehavior;

            if (interactable != null)
                HidePrompt(interactable);
        }

        private void ShowPrompt(InteractableObjectBehavior interactable)
        {
            if (interactable.IsInteractionLocked)
                return;

            if (!_prompts.ContainsKey(interactable.InteractionPrompt))
                return;

            if (_canvas == null)
            {
                _canvas = Object.Instantiate(_inputPromptsPrefabData.InputPromptCanvas.gameObject).GetComponent<Canvas>();
            }

            if (_availablePrompts.Count == 0)
                CreateNewPrompt();

            var prompt = _availablePrompts.Pop();

            prompt.transform.position = interactable.transform.position + 
                new Vector3(interactable.PromptOffset.x, interactable.PromptOffset.y, 0);
            prompt.gameObject.SetActive(true);

            _activePrompts.Add(interactable, prompt);

            UpdatePrompts();
        }

        private void UpdatePrompts()
        {
            foreach (var kvp in _activePrompts)
            {
                var prompt = kvp.Value;
                var interactable = kvp.Key;

                string promptText;

                if (_activeDevice != null && (_activeDevice is Gamepad))
                {
                    promptText = "";

                    var icons = GetIconsDictionary();

                    prompt.SetSprite(icons[_prompts[interactable.InteractionPrompt].GamepadBind]);
                }
                else
                {
                    promptText = _prompts[interactable.InteractionPrompt].KeyboardBind.ToString();
                    prompt.SetSprite(_inputPromptsPrefabData.BlankKey);
                }

                prompt.SetText(promptText);
            }
        }

        private void GetActiveDevice()
        {
            foreach (var device in InputSystem.devices)
                if (device.IsPressed() && _lastActiveDevice != device &&  ((device is Gamepad) || (device is Keyboard)))
                {
                    _lastActiveDevice = _activeDevice;
                    _activeDevice = device;
                    _wasDeviceChanged = true;
                    return;
                }
        }

        private GamepadInputSpriteDictionary GetIconsDictionary()
        {
            if (_activeDevice is UnityEngine.InputSystem.Switch.SwitchProControllerHID)
                return _inputPromptsPrefabData.SwitchIcons;

            if (_activeDevice is UnityEngine.InputSystem.DualShock.DualShockGamepad)
                return _inputPromptsPrefabData.PlayStationIcons;

            return _inputPromptsPrefabData.XBoxIcons;
        }

        private void HidePrompt(InteractableObjectBehavior interactable)
        {
            if (!_activePrompts.ContainsKey(interactable))
                return;

            var prompt = _activePrompts[interactable];
            _activePrompts.Remove(interactable);
            prompt.gameObject.SetActive(false);

            _availablePrompts.Push(prompt);
        }

        private void CreateNewPrompt()
        {
            var prompt = Object.Instantiate(_inputPromptsPrefabData.InputPromptView.gameObject, _canvas.transform).GetComponent<InputPromptView>();
            prompt.gameObject.SetActive(false);
            _availablePrompts.Push(prompt);
        }

        #endregion
    }
}