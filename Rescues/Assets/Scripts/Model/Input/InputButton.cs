using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Rescues
{ 
    public sealed class InputButton : IInput
    {
        #region Fields

        private InputAction _button;
        private bool _isDown;
        private bool _isHeld;
        private bool _isUp;

        private const string KEYPOARD_PATH = "<Keyboard>/";
        private const string GAMEPAD_PATH = "<Gamepad>/";

        #endregion


        #region Properties

        public KeyCode KeyboardBind { get; private set; }
        public GamepadInputs GamepadBind { get; private set; }

        public bool IsDown
        {
            get 
            {
                _isDown = false;

                foreach (var control in _button.controls)
                {
                    _isDown = _isDown || GetButtonControl(control).wasPressedThisFrame;
                }

                return _isDown;
            }
                
        }

        public bool IsHeld
        {
            get
            {
                _isHeld = false;

                foreach (var control in _button.controls)
                {
                    _isHeld = _isHeld || GetButtonControl(control).isPressed;
                }

                return _isHeld;
            }

        }

        public bool IsUp
        {
            get
            {
                _isUp = false;

                foreach (var control in _button.controls)
                {
                    _isUp = _isUp || GetButtonControl(control).wasReleasedThisFrame;
                }

                return _isUp;
            }

        }

        #endregion


        #region ClassLifeCycles

        public InputButton(KeyCode key, GamepadInputs gamepadAction)
        {
            SetNewBind(key, gamepadAction);
        }

        #endregion


        #region Methods

        private ButtonControl GetButtonControl(InputControl control)
        {
            var button = control as ButtonControl;

            if (button == null)
                foreach (var subControl in control.children)
                    GetButtonControl(subControl);

            return button;
        }

        private void SetNewBind(KeyCode key, GamepadInputs gamepadAction)
        {
            _button?.Dispose();

            _button = new InputAction();
            _button.AddBinding(KEYPOARD_PATH + key.ToString());
            KeyboardBind = key;
            _button.AddBinding(GAMEPAD_PATH + GamepadControllPaths.PathsByInputType[gamepadAction]);
            GamepadBind = gamepadAction;
        }

        public void Rebind(KeyCode newKey, GamepadInputs newGamepadAction)
        {
            SetNewBind(newKey, newGamepadAction);
        }

        public void Rebind(KeyCode newKey)
        {
            SetNewBind(newKey, GamepadBind);
        }

        public void Rebind(GamepadInputs newGamepadAction)
        {
            SetNewBind(KeyboardBind, newGamepadAction);
        }

        #endregion


        #region IInput

        public string GetSaveString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(KeyboardBind.ToString());
            sb.Append("|");
            sb.Append(((int)GamepadBind).ToString());

            return sb.ToString();
        }

        public void RebindFromString(string rebindString)
        {
            try
            {
                var newBinds = rebindString.Split('|');

                KeyCode newKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), newBinds[0]);
                GamepadInputs newGamepad = (GamepadInputs)int.Parse(newBinds[1]);

                Rebind(newKey, newGamepad);
            }
            catch (System.Exception e)
            {
                Debug.Log(typeof(InputButton) + "  |  " + e.Message);
            }
        }

        #endregion


        #region IDisposable

        public void Dispose()
        {
            _button.Dispose();
        }

        #endregion
    }
}