using UnityEngine;
using DG.Tweening;


namespace Rescues
{
    public sealed class ItemPutInInventoryController : IInitializeController, ITearDownController
    {

        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public ItemPutInInventoryController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            var items = _context.GetTriggers(InteractableObjectType.Item);
            foreach (var trigger in items)
            {
                var itemBehaviour = trigger as ItemBehaviour;
                itemBehaviour.OnFilterHandler += OnFilterHandler;
                itemBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                itemBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;
            }
        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            var items = _context.GetTriggers(InteractableObjectType.Item);
            foreach (var trigger in items)
            {
                var itemBehaviour = trigger as ItemBehaviour;
                itemBehaviour.OnFilterHandler -= OnFilterHandler;
                itemBehaviour.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                itemBehaviour.OnTriggerExitHandler -= OnTriggerExitHandler;
            }
        }

        #endregion


        #region Methods
        
        private bool OnFilterHandler(Collider2D playerObject)
        {
            return playerObject.CompareTag(TagManager.PLAYER);
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
