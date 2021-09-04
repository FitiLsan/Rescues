using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Rescues
{
    public class PrototypeOntriggerEvent : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<UnityEvent> _onTriggerEnterEvents;
        [SerializeField] private List<float> _enterTimers;
        [SerializeField] private List<UnityEvent> _onTriggerExitEvents;
        [SerializeField] private List<float> _exitTimers;
        [SerializeField] private List<UnityEvent> _onButtonInTriggerEvents;
        [SerializeField] private List<float> _buttonTimers;

        #endregion


        #region Methods

        public void ActivateTriggerEnterEvent()
        {          
            ActivateEvent(_onTriggerEnterEvents, _enterTimers);            
        }

        public void ActivateTriggerExitEvent()
        {
            ActivateEvent(_onTriggerExitEvents, _exitTimers);
        }

        public void ActivateButtonInTriggerEvent()
        {
            ActivateEvent(_onButtonInTriggerEvents, _buttonTimers);
        }     

        public void ActivateEvent(List<UnityEvent> events, List<float> timers)
        {
            for (int i = 0; i < events.Count; i++)
            {
                var time = 0f;
                if (i < timers.Count)
                {
                    time = timers[i];
                }
                events[i].InvokeAfterTime(time);
            }
        }

        #endregion
    } 
}
