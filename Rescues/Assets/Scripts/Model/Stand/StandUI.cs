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
        private Image _standItemImage;

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

        public void Awake()
        {
            _standItemImage = _standItemWindow.GetComponent<Image>();
            if (_standItemSlots != null)
            {               
                for (int i = 0; i < _standItemSlots.Count; i++)
                {
                    _standItemSlots[i].OnPointerClickEvent += OpenStandItemWindow;
                }
            }
        }

        private void OpenStandItemWindow(bool isOpened, Sprite standItemSprite)
        {
            _isItemOpened = isOpened;
            _standItemWindow.SetActive(true);
            _standItemImage.sprite = standItemSprite;
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
                for (int i = 0; i < _standItemSlots.Count; i++)
                {
                    _standItemSlots[i].gameObject.GetComponent<Image>().raycastTarget = true;
                }
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
