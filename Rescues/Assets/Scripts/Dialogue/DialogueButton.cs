using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public class DialogueButton
    {
        public Button button;
        public Text text;
        public RectTransform rect;

        public DialogueButton(Button button, Text text, RectTransform rectTransform)
        {
            this.button = button;
            this.text = text;
            rect = rectTransform;
        }
    }
}
