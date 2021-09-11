using UnityEngine;


namespace Rescues
{
    public class PlayerActivator : MonoBehaviour
    {
        #region Properties

        public OntriggerEvent CurrentTrigger { get; private set; }

        #endregion


        #region UnityMethods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("EditorOnly"))
            {
                CheckIsAlreadyInCollision();
                var curEvent = collision.GetComponent<OntriggerEvent>();
                CurrentTrigger = curEvent;
                curEvent.ActivateTriggerEnterEvent();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var exitedObject = collision.GetComponent<OntriggerEvent>();
            if (CurrentTrigger == exitedObject)
            {
                CheckIsAlreadyInCollision();
            }
        }

        #endregion


        #region Methods

        private void CheckIsAlreadyInCollision()
        {
            if (CurrentTrigger != null)
            {
                Deactivation();
            }
        }

        private void Deactivation()
        {
            CurrentTrigger.ActivateTriggerExitEvent();
            CurrentTrigger = null;
        } 

        #endregion
    } 
}
