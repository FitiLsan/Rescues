using UnityEngine;

namespace Rescues
{
    [CreateAssetMenu(fileName = "InputPromptsPrefabData", menuName = "Data/Input/InputPromptsPrefabData")]
    public class InputPromptsPrefabData : ScriptableObject
    {
        #region Fields

        [SerializeField] private GamepadInputSpriteDictionary _xBoxIcons;
        [SerializeField] private GamepadInputSpriteDictionary _playStationIcons;
        [SerializeField] private GamepadInputSpriteDictionary _switchIcons;
        [SerializeField] private Sprite _blankKey;
        [SerializeField] private InputPromptView _inputPromptView;
        [SerializeField] private Canvas _inputPromptCanvas;

        #endregion


        #region Properties

        public GamepadInputSpriteDictionary XBoxIcons => _xBoxIcons;
        public GamepadInputSpriteDictionary PlayStationIcons => _playStationIcons;
        public GamepadInputSpriteDictionary SwitchIcons => _switchIcons;
        public Sprite BlankKey => _blankKey;
        public InputPromptView InputPromptView => _inputPromptView;
        public Canvas InputPromptCanvas => _inputPromptCanvas;

        #endregion
    }
}