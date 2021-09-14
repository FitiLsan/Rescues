using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public class OntriggerEvent : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<EventData> _onTriggerEnterEvents;
        [SerializeField] private List<EventData> _onTriggerExitEvents;
        [SerializeField] private List<EventData> _onButtonInTriggerEvents;

        #endregion


        #region Methods

        public void ActivateTriggerEnterEvent()
        {          
            ActivateEvent(_onTriggerEnterEvents);            
        }

        public void ActivateTriggerExitEvent()
        {
            ActivateEvent(_onTriggerExitEvents);
        }

        public void ActivateButtonInTriggerEvent()
        {
            ActivateEvent(_onButtonInTriggerEvents);
        }     

        public void ActivateEvent(List<EventData> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                TimeRemainingExtensions.AddTimeRemaining(new TimeRemaining(events[i]));
            }
        }

        #endregion
    } 
}
