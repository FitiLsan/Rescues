using UnityEngine;
using TMPro;

namespace Rescues
{
    public class InputPromptView : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _backgroundImage;
        [SerializeField] TMP_Text _text;

        public void SetText(string text)
        {
            _text.text = text;

            if (_backgroundImage.drawMode == SpriteDrawMode.Sliced)
            {
                var size = _backgroundImage.size;
                size.x = Mathf.Max( text.Length, 1);
                _backgroundImage.size = size;
            }
        }

        public void SetSprite(Sprite sprite)
        {
            _backgroundImage.sprite = sprite;
        }
    }
}
