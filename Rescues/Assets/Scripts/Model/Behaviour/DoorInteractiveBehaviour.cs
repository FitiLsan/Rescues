using UnityEngine;


namespace Rescues
{
    public sealed class DoorInteractiveBehaviour : InteractableObjectBehavior
    {
        #region Fields

        [SerializeField] private Sprite _sprite;
        public ItemData _key;
        private ITrigger _triggerImplementation;

        #endregion

        #region Methods

        public void Open()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _sprite;
        }
        
        #endregion
    }
}
