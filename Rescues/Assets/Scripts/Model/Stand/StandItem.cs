using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


namespace Rescues
{
    public class StandItem : Selectable, IPointerClickHandler, ISubmitHandler
    {
        [SerializeField] StandItemData StandItemData;

        public event Action<bool, Sprite> OnPointerClickEvent;
       
        private Image _image;

        
        public void Awake()
        {
            _image = gameObject.GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CustomDebug.Log("Click!");
            OnPointerClickEvent?.Invoke(true, StandItemData.Sprite);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            CustomDebug.Log("Click!");
            OnPointerClickEvent?.Invoke(true, StandItemData.Sprite);
        }
    }
}