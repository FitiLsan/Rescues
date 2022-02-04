using UnityEngine;

namespace Rescues
{
    [CreateAssetMenu(fileName = "DefaultInputsData", menuName = "Data/Input/DefaultInputsData")]
    public class DefaultInputsData : ScriptableObject
    {
        #region Fields

        public AxisBinds HorizontalAxis;
        public AxisBinds VerticalAxis;
        public ButtonBinds CancelButton;
        public ButtonBinds PickUpButton;
        public ButtonBinds InventoryButton;
        public ButtonBinds NotepadButton;
        public ButtonBinds MouseScrollButton;
        public ButtonBinds UseButton;

        #endregion
    }
}