using UnityEngine;


namespace Rescues
{
    public sealed class DoorInteractiveBehaviour : InteractableObjectBehavior
    {
        #region Fields

        [SerializeField] private Sprite _openSprite;
        [SerializeField] private Sprite _closedSprite;
        [SerializeField] private Collider2D _closedCollider;
        public ItemData _key;
        private ITrigger _triggerImplementation;
        public bool _isClosed = true;
        public bool _isLocked = true;

        #endregion


        #region Methods

        public void OpenClose()
        {
            if (_isClosed)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = _openSprite;
                _closedCollider.enabled = false;
                _isClosed = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = _closedSprite;
                _closedCollider.enabled = true;
                _isClosed = true;
            }
        }

        #endregion
    }
}
