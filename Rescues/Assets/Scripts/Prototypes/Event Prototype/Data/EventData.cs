using System;
using UnityEngine.Events;


namespace Rescues
{
    [Serializable]public struct EventData
    {
        #region Fields

        /// <summary>
        /// Is event repeat after end?
        /// </summary>
        public bool IsRepeating;

        /// <summary>
        /// Time before start
        /// </summary>
        public float Time;

        /// <summary>
        /// Event himself
        /// </summary>
        public UnityEvent Event; 

        #endregion
    }
}
