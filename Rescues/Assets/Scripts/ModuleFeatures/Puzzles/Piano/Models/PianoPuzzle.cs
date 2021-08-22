using System;
using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public class PianoPuzzle : Puzzle
    {
        #region Fields

        public Action<PiannoButton> OnPianoButtonDown;
        public List<PiannoButton> WinCombination;       
        public List<PiannoButton> PlayerCombination;
        public PiannoButton CurrentButton;

        private int _currentIndexInCombination;
        private PiannoButton[] _buttons;

        #endregion


        #region Properties

        public PiannoButton[] Buttons => _buttons;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _buttons = GetComponentsInChildren<PiannoButton>();
        }

        #endregion


        #region Methods

        public void AddToPlayerCombination(PiannoButton button)
        {         
            if (_currentIndexInCombination < WinCombination.Count && button == WinCombination[_currentIndexInCombination])
            {
                _currentIndexInCombination += 1;
                PlayerCombination.Add(button);
                Debug.Log("Button added " + button.name + " current index " + _currentIndexInCombination);
                CheckComplete();
            }
            else
            {
                ResetValues();
            }
        }

        public void SoundPlay(PiannoButton button)
        {
            button.Audio.Play();
        }

        public void ResetPlayerCombination()
        {
            _currentIndexInCombination = 0;
            PlayerCombination.Clear();
        }

        #endregion
    }
}