using UnityEngine;


namespace Rescues
{
    class PianoNavigation : IExecuteController
    {
        #region Fields

        private PianoPuzzle _pianoPuzzle;
        private int _index;
        private bool _right = true;
        private bool _left = false;

        #endregion


        #region Constructors

        public PianoNavigation(PianoPuzzle pianoPuzzle)
        {
            _pianoPuzzle = pianoPuzzle;
            _pianoPuzzle.CurrentButton = _pianoPuzzle.Buttons[_index];
        }

        #endregion


        #region Properties

        public int CurrentButtonIndex
        {
            get => _index;
            set
            {
                if (value >=0 && value < _pianoPuzzle.Buttons.Length)
                {
                    _index = value;
                }
            }
        }

        #endregion


        #region Methods

        public void Execute()
        {
            //TODO неплохо бы перегружать главный ипутконтроллер или вовсе паузить игру, но пока так
            if ((Input.GetKeyUp(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _index < _pianoPuzzle.Buttons.Length - 1)
            {
                SwitchButton(_right);
            }
            else if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _index > 0)
            {
                SwitchButton(_left);
            }
        }

        private void SwitchButton(bool right)
        {
            _pianoPuzzle.CurrentButton.Outline.enabled = false;
            if (right)
            {
                _index += 1;
                _pianoPuzzle.CurrentButton = _pianoPuzzle.Buttons[_index];
                Debug.Log("index " + _index + " current button " + _pianoPuzzle.CurrentButton.name);
            }
            else
            {
                _index -= 1;
                _pianoPuzzle.CurrentButton = _pianoPuzzle.Buttons[_index];
                Debug.Log("index " + _index + " current button " + _pianoPuzzle.CurrentButton.name);
            }
            _pianoPuzzle.CurrentButton.Outline.enabled = true;
        } 

        #endregion
    }
}
