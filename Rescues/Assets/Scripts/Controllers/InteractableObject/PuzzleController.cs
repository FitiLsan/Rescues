using UnityEngine;


namespace Rescues
{
    public sealed class PuzzleController: IInitializeController, ITearDownController
    {
        #region Fields
        
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles
        
        public PuzzleController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController
        
        public void Initialize()
        {
            var puzlles = _context.GetTriggers(InteractableObjectType.Puzzle);
            foreach (var trigger in puzlles)
            {
                var puzlleBehaviour = trigger as PuzzleBehaivour;
                puzlleBehaviour.OnFilterHandler += OnFilterHandler;
                puzlleBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                puzlleBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;
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
