using System;
using UnityEngine;


namespace Rescues
{
    public sealed class DoorTeleporterBehaviour : MonoBehaviour, IOnTrigger
    {
        #region Properties
        
        public Predicate<Collider2D> OnFilterHandler { get; set; }
        public Action<IOnTrigger> OnTriggerEnterHandler { get; set; }
        public Action<IOnTrigger> OnTriggerExitHandler { get; set; }
        public bool IsInteractable { get; set; }
        public GameObject GameObject => gameObject;

        #endregion


        #region Fields

        [SerializeField] Transform ExitPoint;

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
