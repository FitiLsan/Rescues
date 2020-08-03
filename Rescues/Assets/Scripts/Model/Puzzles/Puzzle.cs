using System;
using UnityEngine;


namespace Rescues
{
    public abstract class Puzzle : MonoBehaviour, IPuzzle
    {
        #region Fileds

        public event Action PuzzleHasBeenFinished = () => { };

        #endregion


        #region Prperties

        public bool IsFinished { get; private set; } = false;

        #endregion


        #region Methods

        [ContextMenu("Activate puzzle")]
        public void Activate()
        {
            if (!IsFinished)
                gameObject.SetActive(true);
            //TODO Надо как-то останавливать игру, делать паузу? Или перехватывать управление?
        }

        public void Close()
        {
            ResetValues();
            gameObject.SetActive(false);
        }

        public void Finish()
        {
            IsFinished = true;
            PuzzleHasBeenFinished.Invoke();
            Close();
        }

        public abstract void ResetValues();

        #endregion
    }
}