using UnityEngine;


namespace Rescues
{
    class PianoUse : IExecuteController
    {
        #region Fields

        private PianoPuzzle _pianoPuzzle;

        #endregion


        #region Constructors

        public PianoUse(PianoPuzzle pianoPuzzle)
        {
            _pianoPuzzle = pianoPuzzle;
        }

        #endregion


        #region Methods

        public void Execute()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _pianoPuzzle.OnPianoButtonDown?.Invoke(_pianoPuzzle.CurrentButton);
            }
        } 

        #endregion
    }
}
