using System;
using UnityEngine;


namespace Rescues
{
    public abstract class Puzzle : MonoBehaviour
    {
        #region Fileds
        
        public event Action<Puzzle> Activated = puzzle => { };
        public event Action<Puzzle> Closed = puzzle => { };
        public event Action<Puzzle> Finished = puzzle => { };
        public event Action<Puzzle> ResetValuesToDefault = puzzle => { };
        public event Action<Puzzle> CheckCompleted = puzzle => { };

        public bool IsFinished = false;

        #endregion


        #region Methods

        [ContextMenu("Activate puzzle")]
        public void Activate()
        {
            Activated.Invoke(this);
        }

        public void Close()
        {
            Closed.Invoke(this);
        }

        public void Finish()
        {
            Finished.Invoke(this);
        }

        public void ResetValues()
        {
            ResetValuesToDefault.Invoke(this);
        }

        public void CheckComplete()
        {
            CheckCompleted.Invoke(this);
        }
        
        #endregion
    }
}