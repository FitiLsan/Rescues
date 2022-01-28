using TMPro;
using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "BasicWrite", menuName = "Data/WritePatterns/BasicWrite")]
    public sealed class BasicWrite : WritePattern
    {
        #region Methods

        public override void DrawText(string inputText, TextMeshProUGUI outputTextContainer)
        {
            ClearText(outputTextContainer);

            outputTextContainer.text += inputText;
        }

        public override void ClearText(TextMeshProUGUI textContainer)
        {
            textContainer.text = string.Empty;
        } 

        #endregion
    }
}