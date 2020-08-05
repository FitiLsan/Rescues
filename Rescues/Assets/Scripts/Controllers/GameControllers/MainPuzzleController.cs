using UnityEngine;


namespace Rescues
{
    public sealed class MainPuzzleController : IInitializeController, ITearDownController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public MainPuzzleController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            var puzzleInteracts = _context.GetTriggers(InteractableObjectType.Puzzle);
            var canvas = Object.FindObjectOfType<Canvas>();
            var puzzleControllers = new PuzzlesControllers();

            foreach (var trigger in puzzleInteracts)
            {
                var puzlleBehaviour = trigger as PuzzleBehaivour;
                puzlleBehaviour.OnFilterHandler += OnFilterHandler;
                puzlleBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                puzlleBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;

                foreach (var somePuzzleController in puzzleControllers.ControllersList)
                {
                    if (somePuzzleController.Value == puzlleBehaviour.Puzzle.GetType())
                    {
                        var puzzleInstance = GameObject.Instantiate(puzlleBehaviour.Puzzle, canvas.transform);
                        puzlleBehaviour.Puzzle = puzzleInstance;
                        somePuzzleController.Key.Initialize(puzzleInstance);
                    }
                }
            }
        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            var puzlles = _context.GetTriggers(InteractableObjectType.Puzzle);
            foreach (var trigger in puzlles)
            {
                var puzlleBehaviour = trigger as PuzzleBehaivour;
                puzlleBehaviour.OnFilterHandler -= OnFilterHandler;
                puzlleBehaviour.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                puzlleBehaviour.OnTriggerExitHandler -= OnTriggerExitHandler;
            }
        }

        #endregion


        #region Methods

        private bool OnFilterHandler(Collider2D obj)
        {
            return obj.CompareTag(TagManager.PLAYER);
        }

        private void OnTriggerEnterHandler(ITrigger enteredObject)
        {
            enteredObject.IsInteractable = true;
        }

        private void OnTriggerExitHandler(ITrigger enteredObject)
        {
            enteredObject.IsInteractable = false;
        }

        #endregion
    }
}