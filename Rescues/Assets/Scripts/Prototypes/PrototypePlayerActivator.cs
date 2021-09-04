using UnityEngine;


namespace Rescues
{
    public class PrototypePlayerActivator : MonoBehaviour
    {
        public PrototypeOntriggerEvent CurrentTrigger { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("EditorOnly"))
            {
                CheckIsAlreadyInCollision();
                var curEvent = collision.GetComponent<PrototypeOntriggerEvent>();
                CurrentTrigger = curEvent;
                curEvent.ActivateTriggerEnterEvent();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var exitedObject = collision.GetComponent<PrototypeOntriggerEvent>();
            if (CurrentTrigger == exitedObject)
            {
                CheckIsAlreadyInCollision();
            }
        }

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
    } 
}
