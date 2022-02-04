using System;
using System.Collections;
using UnityEngine;


namespace Rescues
{
    /// <summary>
    /// От этого класса наследуются модели пазлов
    /// </summary>
    public abstract class Puzzle : MonoBehaviour
    {
        #region Fileds

        [SerializeField] private float _delayAfterFinish = 3f;
        public event Action<Puzzle> Activated = puzzle => { };
        public event Action<Puzzle> Closed = puzzle => { };
        public event Action<Puzzle> Finished = puzzle => { };
        public event Action<Puzzle> ResetValuesToDefault = puzzle => { };
        public event Action<Puzzle> CheckCompleted = puzzle => { };

        public bool IsFinished = false;
        public bool IsActive = false;

        #endregion


        #region Methods

        [ContextMenu("Activate puzzle")]
        public void Activate()
        {
            if (IsFinished || IsActive) return;
            Activated.Invoke(this);
            IsActive = true;
        }

        public void Close()
        {        
            if (_delayAfterFinish > 0)
            {
                TimeRemainingExtensions.AddTimeRemaining(new TimeRemaining(CloseWithDelay, _delayAfterFinish));
            }
            else
            {
                gameObject.SetActive(false);
            }

            Closed.Invoke(this);
            IsActive = false;
        }

        public void Finish()
        {
            Finished.Invoke(this);
            IsFinished = true;
        }

        public void ResetValues()
        {
            ResetValuesToDefault.Invoke(this);
        }

        public void CheckComplete()
        {
            CheckCompleted.Invoke(this);
        }

        public void ForceClose()
        {
            Closed.Invoke(this);
            gameObject.SetActive(false);
            IsActive = false;
        }

        private void CloseWithDelay()
        {
            gameObject.SetActive(false);
        }
        
        #endregion
    }
}