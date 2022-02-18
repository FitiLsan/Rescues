using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Rescues
{
    public sealed class StandItem : MonoBehaviour, IPointerClickHandler
    {
        #region Fields

        public event Action<StandItemData> OnPointerClickEvent;

        [SerializeField] private StandItemData StandItemData;

        #endregion


        #region Methods

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickEvent?.Invoke(StandItemData);
        }

        #endregion
    }
}