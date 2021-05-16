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
        [SerializeField] List<string> _dontNeedItemPhrases;
        [SerializeField] GameObject _standItemWindow;
        [SerializeField] private bool _isItemOpened = false;
        private bool _isMouseIn = true;
        private ItemData _item;
        private Image _standItemImage;
        private int _slotNumber;
        private System.Random _random = new System.Random();

        #endregion


        #region Properties

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

        #endregion


        #region UnityMethods

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

        #endregion


        #region Methods

        private void OpenStandItem(int number, StandItemData standItem)
        {
            if (standItem.CanBeTaken)
            {
                _item = standItem.Item;
            }
            else
            {
                PlayDontNeedItem();
            }
            _slotNumber = number;
            _standItemImage.sprite = standItem.Sprite;
            _standItemWindow.SetActive(true);
        }

        public void OpenStandItemWindow()
        {
            _isItemOpened = true;
            Debug.Log("опен итем");
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
                Debug.Log("в ui");
                _standItemWindow.SetActive(false);
                for (int i = _standItemSlots.Count - 1; i > 0; i--)
                {
                    _standItemSlots[i].gameObject.GetComponent<Image>().raycastTarget = true;
                }
                _item = null;
            }
        }

        public void PlayDontNeedItem()
        {
            int temp = _random.Next(_dontNeedItemPhrases.Count);
            CustomDebug.Log(_dontNeedItemPhrases[temp]);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isMouseIn = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isMouseIn = true;
        }

        #endregion
    }
}
