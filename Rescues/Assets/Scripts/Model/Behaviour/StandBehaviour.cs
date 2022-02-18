using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public sealed class StandBehaviour : InteractableObjectBehavior
    {
        #region Fields

        public GameObject StandWindow;

        [SerializeField] private List<StandItem> _standItemSlots;
        [SerializeField] private Image _standItemImage;

        #endregion


        #region Properties

        public List<StandItem> StandItemSlots => _standItemSlots;
        public Image StandItemImage => _standItemImage;

        #endregion


        #region UnityMethods

        private void OnValidate()
        {
            Type = InteractableObjectType.Stand;
        }

        #endregion
    }
}
