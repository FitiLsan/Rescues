using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rescues
{
    public class StandUI : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        #region Fields

        [SerializeField] List<StandItem> _standItemSlots;
        [SerializeField] GameObject _standItemWindow;
        private bool _isItemOpened = false;
        private bool _isMouseIn = true;
        private ItemData _item;
        private Image _standItemImage;
        private int _slotNumber;

        #endregion

        public bool IsItemOpened
        {
            get { return _isItemOpened; }
        }

        public bool IsMouseIn
        {
            get { return _isMouseIn; }
        }

        public List<StandItem> StandItemSlots
        {
            get { return _standItemSlots; }
        }

        public ItemData Item
        {
            get { return _item; }
        }

        public int SlotNumber
        {
            get { return _slotNumber; }
        }


        public void Awake()
        {
            _standItemImage = _standItemWindow.GetComponent<Image>();
            if (_standItemSlots != null)
            {
                for (int i = 0; i < _standItemSlots.Count; i++)
                {
                    _standItemSlots[i].OnPointerClickEvent += OpenStandItem;
                    _standItemSlots[i].ItemSlotNumber = i;
                }
            }
        }

        private void OpenStandItem(int number, StandItemData standItem)
        {            
            if (standItem.CanBeTaken)
            {
                _item = standItem.Item;              
            }
            _slotNumber = number;
            _standItemImage.sprite = standItem.Sprite;
        }

        public void OpenStandItemWindow()
        {
            _isItemOpened = true;         
            _standItemWindow.SetActive(true);
            for (int i = 0; i < _standItemSlots.Count; i++)
            {
                _standItemSlots[i].gameObject.GetComponent<Image>().raycastTarget = false;
            }
        }

        public void CloseStandItemWindow()
        {
            if (_isItemOpened)
            {
                _isItemOpened = false;
                _standItemWindow.SetActive(false);
                for (int i = _standItemSlots.Count-1; i > 0; i--)
                {
                    _standItemSlots[i].gameObject.GetComponent<Image>().raycastTarget = true;
                }
                _item = null;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isMouseIn = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isMouseIn = true;
        }
    }
}
