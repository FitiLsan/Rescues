namespace Rescues
{
    public interface IPuzzleController : IController
    {
        void Initialize(Puzzle puzzle, PuzzleBehaviour puzzleBehaviour);
        void Activate(Puzzle puzzle);
        void Close(Puzzle puzzle);
        void CheckComplete(Puzzle puzzle);
        void Finish(Puzzle puzzle);
        void ResetValues(Puzzle puzzle);
    }
}