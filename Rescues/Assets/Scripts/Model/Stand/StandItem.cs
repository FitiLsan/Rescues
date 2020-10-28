using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


namespace Rescues
{
    public class StandItem : Selectable, IPointerClickHandler, ISubmitHandler
    {
        [SerializeField] StandItemData StandItemData;

        public event Action<int, StandItemData> OnPointerClickEvent;
       
        private Image _image;

        private int _itemSlotNumber; 

        public int ItemSlotNumber
        {
            set { _itemSlotNumber = value; }
        }

        
        protected override void Awake()
        {
            base.Awake();
            _image = gameObject.GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CustomDebug.Log("Click!");
            OnPointerClickEvent?.Invoke(_itemSlotNumber, StandItemData);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            CustomDebug.Log("Click!");
            OnPointerClickEvent?.Invoke(_itemSlotNumber, StandItemData);
        }
    }
}