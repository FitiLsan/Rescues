using System.Collections.Generic;


namespace Rescues
{
    public class PianoController : IPuzzleController, IExecuteController
    {
        #region Fields

        private PianoNavigation _navigation;
        private PianoUse _useController;
        private List<IExecuteController> _pianoExecuteControllers;

        #endregion


        #region Methods

        public void Initialize(Puzzle puzzle)
        {
            var pianoPuzzle = puzzle as PianoPuzzle;
            pianoPuzzle.gameObject.SetActive(false);
            pianoPuzzle.Activated += Activate;
            pianoPuzzle.Closed += Close;
            pianoPuzzle.Finished += Finish;
            pianoPuzzle.CheckCompleted += CheckComplete;
            pianoPuzzle.ResetValuesToDefault += ResetValues;

            pianoPuzzle.OnPianoButtonDown += pianoPuzzle.SoundPlay;
            pianoPuzzle.OnPianoButtonDown += pianoPuzzle.AddToPlayerCombination;

            _navigation = new PianoNavigation(pianoPuzzle);
            _useController = new PianoUse(pianoPuzzle);
            _pianoExecuteControllers = new List<IExecuteController>();
        }

        public void Activate(Puzzle puzzle)
        {
            if (puzzle.IsFinished) return;

            _pianoExecuteControllers.Add(_navigation);
            _pianoExecuteControllers.Add(_useController);
            puzzle.gameObject.SetActive(true);         
        }

        public void Close(Puzzle puzzle)
        {
            puzzle.gameObject.SetActive(false);
            _pianoExecuteControllers.Clear();
            puzzle.ResetValues();
        }

        public void Finish(Puzzle puzzle)
        {
            var pianoPuzzle = puzzle as PianoPuzzle;
            pianoPuzzle.OnPianoButtonDown -= pianoPuzzle.AddToPlayerCombination;
            pianoPuzzle.OnPianoButtonDown -= pianoPuzzle.SoundPlay;

            puzzle.IsFinished = true;
            puzzle.Close();
            UnityEngine.Debug.Log("piano puzzle finish");
        }

        public void CheckComplete(Puzzle puzzle)
        {
            var pianoPuzzle = puzzle as PianoPuzzle;
            if (pianoPuzzle.PlayerCombination.Count == pianoPuzzle.WinCombination.Count)
            {
                UnityEngine.Debug.Log("piano puzzle check");
                Finish(puzzle);                
            }
        }

        public void ResetValues(Puzzle puzzle)
        {
            var pianoPuzzle = puzzle as PianoPuzzle;
            pianoPuzzle.ResetPlayerCombination();           
        }

        public void Execute()
        {
            if (_pianoExecuteControllers != null && _pianoExecuteControllers.Count > 0)
            {
                foreach (var controller in _pianoExecuteControllers)
                {
                    controller.Execute();
                } 
            }
        }

        #endregion
    }
}
