using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotatingCircle : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        print("clicked");
        var image = GetComponent<Image>();
        var color = image.color;
        color.a = 0.3f;
        image.color = color;
    }
}
