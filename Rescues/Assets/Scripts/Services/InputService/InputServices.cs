using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Rescues
{
    public sealed class InputServices : Service
    {
        #region Fields

        private List<IInput> _inputs;
        private DefaultInputsData _defaultInputsData;

        #endregion


        #region Properties

        public InputAxis HorizontalAxis { get; private set; }
        public InputAxis VerticalAxis { get; private set; }
        public InputButton CancelButton { get; private set; }
        public InputButton PickUpButton { get; private set; }
        public InputButton InventoryButton { get; private set; }
        public InputButton NotepadButton { get; private set; }
        public InputButton MouseScrollButton { get; private set; }
        public InputButton UseButton { get; private set; }

        #endregion


        #region ClassLifeCycles

        public InputServices(Contexts contexts) : base(contexts)
        {
            _defaultInputsData = Resources.Load<DefaultInputsData>(AssetsPathGameObject.InputData[InputDataType.DefaultInputsData]);

            SetDefaultInputs();

            _inputs = new List<IInput>(8);
            _inputs.Add(HorizontalAxis);
            _inputs.Add(VerticalAxis);
            _inputs.Add(CancelButton);
            _inputs.Add(PickUpButton);
            _inputs.Add(InventoryButton);
            _inputs.Add(NotepadButton);
            _inputs.Add(MouseScrollButton);
            _inputs.Add(UseButton);
        }

        #endregion


        #region Methods

        public void SetDefaultInputs()
        {
            HorizontalAxis = new InputAxis(_defaultInputsData.HorizontalAxis.Positive.Key, _defaultInputsData.HorizontalAxis.Negative.Key,
                _defaultInputsData.HorizontalAxis.Positive.GamepadInput, _defaultInputsData.HorizontalAxis.Negative.GamepadInput);

            VerticalAxis = new InputAxis(_defaultInputsData.VerticalAxis.Positive.Key, _defaultInputsData.VerticalAxis.Negative.Key,
                _defaultInputsData.VerticalAxis.Positive.GamepadInput, _defaultInputsData.VerticalAxis.Negative.GamepadInput);

            CancelButton = new InputButton(_defaultInputsData.CancelButton.Key, _defaultInputsData.CancelButton.GamepadInput);
            PickUpButton = new InputButton(_defaultInputsData.PickUpButton.Key, _defaultInputsData.PickUpButton.GamepadInput);
            InventoryButton = new InputButton(_defaultInputsData.InventoryButton.Key, _defaultInputsData.InventoryButton.GamepadInput);
            NotepadButton = new InputButton(_defaultInputsData.NotepadButton.Key, _defaultInputsData.NotepadButton.GamepadInput);
            MouseScrollButton = new InputButton(_defaultInputsData.MouseScrollButton.Key, _defaultInputsData.MouseScrollButton.GamepadInput);
            UseButton = new InputButton(_defaultInputsData.UseButton.Key, _defaultInputsData.UseButton.GamepadInput);
        }

        public string GetSaveString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _inputs.Count; i++)
            {
                sb.Append(_inputs[i].GetSaveString());

                if (i != _inputs.Count -1)
                    sb.Append(";");
            }

            return sb.ToString();
        }

        public void LoadInputsFromString(string inputString)
        {
            try
            {
                var newBinds = inputString.Split(';');

                if (newBinds.Length != _inputs.Count)
                    return;

                for (int i = 0; i < _inputs.Count; i++)
                {
                        Debug.Log(newBinds[i]);
                        _inputs[i].RebindFromString(newBinds[i]);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(typeof(InputServices) + "  |  " + e.Message + "\n" +
                    "Incorrect input string. Controls were not rebound");
            }
        }

        #endregion
    }
}