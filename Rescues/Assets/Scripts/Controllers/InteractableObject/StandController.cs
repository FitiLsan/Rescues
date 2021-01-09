using DG.Tweening;
using UnityEngine;


namespace Rescues
{
    public sealed class StandController : IInitializeController, ITearDownController
    {
        #region Fields
        
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles
        
        public StandController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController
        
        public void Initialize()
        {
            var stands = _context.GetTriggers(InteractableObjectType.Stand);
            foreach (var trigger in stands)
            {
                var standsBehaviour = trigger as StandBehaviour;
                standsBehaviour.OnFilterHandler += OnFilterHandler;
                standsBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                standsBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;

                standsBehaviour.StandWindow.gameObject.SetActive(false);
            }
        }

        #endregion


        #region ITearDownController
        
        public void TearDown()
        {
            var stands = _context.GetTriggers(InteractableObjectType.Stand);
            foreach (var trigger in stands)
            {
                var standsBehaviour = trigger as StandBehaviour;
                standsBehaviour.OnFilterHandler -= OnFilterHandler;
                standsBehaviour.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                standsBehaviour.OnTriggerExitHandler -= OnTriggerExitHandler;
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
            var materialColor = enteredObject.GameObject.GetComponent<SpriteRenderer>().color;
            enteredObject.GameObject.GetComponent<SpriteRenderer>().DOColor(new Color(materialColor.r,
                materialColor.g, materialColor.b, 0.5f), 1.0f);
        }

        private void OnTriggerExitHandler(ITrigger enteredObject)
        {
            enteredObject.IsInteractable = false;
            var materialColor = enteredObject.GameObject.GetComponent<SpriteRenderer>().color;
            enteredObject.GameObject.GetComponent<SpriteRenderer>().DOColor(new Color(materialColor.r,
                materialColor.g, materialColor.b, 1.0f), 1.0f);
        }

        #endregion
    }
}
