using UnityEngine;


public class PrototypePlayerActivator : MonoBehaviour {
    private PrototypeOntriggerEvent CurrentPrototypeTrigger;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("EditorOnly")) {
            CheckIsAlreadyInCollision();
            var curEvent = collision.GetComponent<PrototypeOntriggerEvent>();
            CurrentPrototypeTrigger = curEvent;
            curEvent.ActivateTriggerEnterEvent(); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        var exitedObject = collision.GetComponent<PrototypeOntriggerEvent>();
        if (CurrentPrototypeTrigger == exitedObject) {
            CheckIsAlreadyInCollision();
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Use")) {
            if (CurrentPrototypeTrigger != null) {
                CurrentPrototypeTrigger.ActivateButtonInTriggerEvent();
            }
        }
    }

    private void CheckIsAlreadyInCollision() {
        if (CurrentPrototypeTrigger != null) {
            Deactivation();
        }
    }

    private void Deactivation() {
        CurrentPrototypeTrigger.ActivateTriggerExitEvent();
        CurrentPrototypeTrigger = null;
    }
}
