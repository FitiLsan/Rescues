using UnityEngine;

namespace Rescues
{
    public class WiresController : IPuzzleController
    {
        
        #region IPuzzleController
        
        public void Initialize(Puzzle puzzle)
        {
            puzzle.Activated += Activate;
            puzzle.Closed += Close;
            puzzle.Finished += Finish;
            puzzle.CheckCompleted += CheckComplete;
            puzzle.ResetValuesToDefault += ResetValues;
            
            Close(puzzle);
        }

        public void Activate(Puzzle puzzle)
        {
            if (puzzle.IsFinished) return;
            
            var puzzlePosition = Camera.main.transform.position;
            //изощерения с Z из-за непонятности с камерой и расположением объектов на сцене
            puzzlePosition.z += 2;
            puzzle.transform.position = puzzlePosition;
            puzzle.gameObject.SetActive(true);
            
            //TODO Надо как-то останавливать игру, делать паузу? Или перехватывать управление?
        }

        public void Close(Puzzle puzzle)
        {
            puzzle.gameObject.SetActive(false);
        }

        public void Finish(Puzzle puzzle)
        {
            puzzle.IsFinished = true;
            Close(puzzle);
        }

        public void CheckComplete(Puzzle puzzle)
        {
            var specificPuzzle = puzzle as WiresPuzzle;
            if (specificPuzzle != null)
            {
                var checkCounter = 0;
                for (int i = 0; i < specificPuzzle.Connectors.Count; i++)
                {
                    if (specificPuzzle.Connectors[i].IsCorrectWire)
                        checkCounter++;
                }

                if (checkCounter == specificPuzzle.Connectors.Count - 1)
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