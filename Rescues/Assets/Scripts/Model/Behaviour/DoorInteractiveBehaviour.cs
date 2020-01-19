using System;
using UnityEngine;


namespace Rescues
{
    public sealed class DoorInteractiveBehaviour : MonoBehaviour, ITrigger
    {
        #region Properties
        
        public Predicate<Collider2D> OnFilterHandler { get; set; }
        public Action<ITrigger> OnTriggerEnterHandler { get; set; }
        public Action<ITrigger> OnTriggerExitHandler { get; set; }
        public bool IsInteractable { get; set; }
        public GameObject GameObject => gameObject;

        #endregion


        #region Fields

        [SerializeField] public Sprite DoorSprite;

        #endregion


        #region UnityMethods

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (OnFilterHandler.Invoke(other))
            {
                OnTriggerEnterHandler.Invoke(this);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (OnFilterHandler.Invoke(other))
            {
                OnTriggerExitHandler.Invoke(this);
            }
        }

        #endregion
    }
}
