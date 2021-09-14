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
                var curEvent = collision.GetComponent<OntriggerEvent>();
                CurrentTrigger = curEvent;
                curEvent.ActivateTriggerEnterEvent();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var exitedObject = collision.GetComponent<OntriggerEvent>();
            if (CurrentTrigger == exitedObject && CurrentTrigger != null)
            {
                CurrentTrigger.ActivateTriggerExitEvent();
                CurrentTrigger = null;
            }
        }

        #endregion
    } 
}
