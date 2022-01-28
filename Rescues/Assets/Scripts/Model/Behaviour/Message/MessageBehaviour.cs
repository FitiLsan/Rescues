using TMPro;
using UnityEngine;


namespace Rescues
{
    public class MessageBehaviour : MonoBehaviour
    {
        #region Fields

        public string inputText;
        public WritePattern writePattern;
        public TextMeshProUGUI outputTextContainer;

        #endregion


        [field: SerializeField] public bool IsLocked { get; set; }


        #region UnityMethods

        private void Awake()
        {
            if (outputTextContainer == null)
            {
                outputTextContainer = GetComponent<TextMeshProUGUI>(); 
            }

            if (writePattern == null)
            {
                writePattern = Resources.Load<BasicWrite>(AssetsPathGameObject.Object
                    [GameObjectType.BasicWritePattern]);
#if UNITY_EDITOR
                Debug.Log($"Seems like someone forgot to chose writePattern in {name}. So i chose {writePattern.name}.");
#endif
            }
        }

        #endregion


        #region Methods

        public void Show()
        {
            if (IsLocked == false)
            {
                writePattern.DrawText(inputText, outputTextContainer);
            }
        }

        public void Hide()
        {
            if (IsLocked == false)
            {
                writePattern.ClearText(outputTextContainer);
            }
        }

        #endregion
    }
}
