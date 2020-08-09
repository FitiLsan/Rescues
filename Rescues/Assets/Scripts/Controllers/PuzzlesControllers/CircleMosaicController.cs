using UnityEngine;

namespace Rescues
{
    public class CircleMosaicController : IPuzzleController
    {

        #region IPuzzleController

        public void Initialize(Puzzle puzzle, PuzzleBehaviour puzzleBehaviour)
        {
            puzzle.Activated += Activate;
            puzzle.Closed += Close;
            puzzle.Finished += Finish;
            puzzle.CheckCompleted += CheckComplete;
            puzzle.ResetValuesToDefault += ResetValues;

            var circlePuzzle = puzzle as CircleMosaicPuzzle;
            circlePuzzle.Initialize(puzzleBehaviour.PuzzleData as CircleMosaicData);
            Close(puzzle);
        }

        public void Activate(Puzzle puzzle)
        {
            CustomDebug.Log(123);
            if (!puzzle.IsFinished)
                puzzle.gameObject.SetActive(true);
            //TODO Надо как-то останавливать игру, делать паузу? Или перехватывать управление?
        }

        public void Close(Puzzle puzzle)
        {
            puzzle.gameObject.SetActive(false);
        }

        public void Finish(Puzzle puzzle)
        {
            Debug.Log("Finished");
            puzzle.IsFinished = true;
            Close(puzzle);
        }

        public void CheckComplete(Puzzle puzzle)
        {
            var specificPuzzle = puzzle as CircleMosaicPuzzle;
            if (specificPuzzle != null)
            {
                var uncompleteCircles = specificPuzzle.Circles.FindAll(e => Mathf.Abs(e.Angle % 360) != 0);
                if (uncompleteCircles.Count == 0)
                    Finish(specificPuzzle);
            }
        }

        public void ResetValues(Puzzle puzzle)
        {
            //TODO
        }

        #endregion
    }
}