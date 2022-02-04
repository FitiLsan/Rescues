using System.Collections.Generic;


namespace Rescues
{
    public sealed class EventSystemBehaviour : InteractableObjectBehavior
    {
        #region Fields

        public List<EventData> OnTriggerEnterEvents;
        public List<EventData> OnTriggerExitEvents;
        public List<EventData> OnButtonInTriggerEvents;

        #endregion


        #region Methods

        public void ActivateButtonInTriggerEvent()
        {
            ActivateEvents(OnButtonInTriggerEvents);
        }

        public void ActivateEvents(List<EventData> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                ActivateEvent(data[i]);
            }
        }

        public int LockEventsByIDs(string[] commandValues)
        {
            int completedCommands = 0;
            for (int j = 0; j < commandValues.Length; j++)
            {
                for (int i = 0; i < OnTriggerEnterEvents.Count; i++)
                {
                    if (OnTriggerEnterEvents[i].Id == commandValues[j])
                    {
                        OnTriggerEnterEvents[i].IsInteractionLocked = !OnTriggerEnterEvents[i].IsInteractionLocked;
                        ActivateEvent(OnTriggerEnterEvents[i]);
                        completedCommands++;
                        break;
                    }
                }

                for (int i = 0; i < OnTriggerExitEvents.Count; i++)
                {
                    if (OnTriggerExitEvents[i].Id == commandValues[j])
                    {
                        OnTriggerExitEvents[i].IsInteractionLocked = !OnTriggerExitEvents[i].IsInteractionLocked;
                        completedCommands++;
                        break;
                    }
                }

                for (int i = 0; i < OnButtonInTriggerEvents.Count; i++)
                {
                    if (OnButtonInTriggerEvents[i].Id == commandValues[j])
                    {
                        OnButtonInTriggerEvents[i].IsInteractionLocked = !OnButtonInTriggerEvents[i].
                            IsInteractionLocked;
                        completedCommands++;
                        break;
                    }
                }
            }

            return completedCommands;
        }

        private void ActivateEvent(EventData data)
        {
            if (data.IsInteractionLocked == false)
            {
                ExecuteAdvancedOptions(data);
                TimeRemainingExtensions.AddTimeRemaining(new TimeRemaining(data));
            }
        }

        private void ExecuteAdvancedOptions(EventData data)
        {
            foreach (KeyValuePair<EventSystemCommands, string> command in data.objectManipulations)
            {
                switch (command.Key)
                {
                    case EventSystemCommands.LockEventByID:
                        {
                            LockEventsByIDs(ToStringArray(command.Value));
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private string[] ToStringArray(string stringToConvert)
        {
            List<string> strings = new List<string>();
            char[] delimeters = { ',', '-', '_', ' ' };
            string[] split = stringToConvert.Split(delimeters);
            for (int i = 0; i < split.Length; i++)
            {
                if (string.IsNullOrEmpty(split[i]) == false)
                {
                    strings.Add(split[i]);
                }
            }

            return strings.ToArray();
        }

        #endregion


        #region UnityMethods

        private void OnValidate()
        {
            Type = InteractableObjectType.EventSystem;
        }

        #endregion
    }
}
