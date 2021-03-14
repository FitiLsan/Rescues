using UnityEngine;


public class PrototypePlayerActivator : MonoBehaviour
{
    [SerializeField] private PrototypeOntriggerEvent CurrentPrototypeTrigger;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("EditorOnly")) {

            var curEvent = collision.GetComponent<PrototypeOntriggerEvent>();
            CurrentPrototypeTrigger = curEvent;
            curEvent.ActivateTriggerEnterEvent();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("EditorOnly")) {
            CurrentPrototypeTrigger.ActivateTriggerExitEvent();
            CurrentPrototypeTrigger = null;
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Use")) {
            if (CurrentPrototypeTrigger != null) {
                PrototypeOntriggerEvent prototypeTrigger = CurrentPrototypeTrigger.GetComponent<PrototypeOntriggerEvent>();
                prototypeTrigger.ActivateButtonInTriggerEvent();
            }
        }
    }
}
