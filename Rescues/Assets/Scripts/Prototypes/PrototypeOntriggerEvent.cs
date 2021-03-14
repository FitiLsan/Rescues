using UnityEngine;
using UnityEngine.Events;

public class PrototypeOntriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTriggerEntetEvent;
    [SerializeField] private UnityEvent OnTriggerExitEvent;
    [SerializeField] private UnityEvent OnButtonInTriggerEvent;

    private void Start()
    {
        if (OnTriggerEntetEvent == null) OnTriggerEntetEvent = new UnityEvent();
        if (OnTriggerExitEvent == null) OnTriggerExitEvent = new UnityEvent();
        if (OnButtonInTriggerEvent == null) OnButtonInTriggerEvent = new UnityEvent();
    }

    public void ActivateTriggerEnterEvent() 
    {
        OnTriggerEntetEvent.Invoke();
    }

    public void ActivateTriggerExitEvent() {
        OnTriggerExitEvent.Invoke();
    }

    public void ActivateButtonInTriggerEvent() 
    {
        OnButtonInTriggerEvent.Invoke();
    }
}
