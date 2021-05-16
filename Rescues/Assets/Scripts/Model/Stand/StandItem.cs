using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


namespace Rescues
{
    public class StandItem : Selectable, IPointerClickHandler, ISubmitHandler
    {
        #region Fields

        [SerializeField] StandItemData StandItemData;
        public event Action<int, StandItemData> OnPointerClickEvent;
        private Image _image;
        [SerializeField] private int _itemSlotNumber;

        #endregion


        #region Properties

        public int ItemSlotNumber
        {
            set { _itemSlotNumber = value; }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _image = gameObject.GetComponent<Image>();
        }

        #endregion


        #region Methods

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

        #endregion
    }
}