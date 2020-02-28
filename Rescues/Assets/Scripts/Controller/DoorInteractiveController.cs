using DG.Tweening;
using UnityEngine;


namespace Rescues
{
    public sealed class DoorInteractiveController : IInitializeController, ITearDownController
    {
        #region Fields
        
        private readonly GameContext _context;
        private ItemData _key;

        #endregion


        #region ClassLifeCycles

        public DoorInteractiveController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController
        
        public void Initialize()
        {
            var doors = _context.GetTriggers(InteractableObjectType.Door);
            foreach (var trigger in doors)
            {
                var doorInteractiveBehaviour = trigger as DoorInteractiveBehaviour;
                doorInteractiveBehaviour.OnFilterHandler += OnFilterHandler;
                doorInteractiveBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                doorInteractiveBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;
            }
        }

        #endregion


        #region ITearDownController
        
        public void TearDown()
        {
            var doors = _context.GetTriggers(InteractableObjectType.Door);
            foreach (var trigger in doors)
            {
                var doorInteractiveBehaviour = trigger as DoorInteractiveBehaviour;
                doorInteractiveBehaviour.OnFilterHandler -= OnFilterHandler;
                doorInteractiveBehaviour.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                doorInteractiveBehaviour.OnTriggerExitHandler -= OnTriggerExitHandler;
            }
        }

        #endregion


        #region Methods

        public void ItemCheck()
        {
            if (_context.Inventory.GetItem(_key))
            {
                OpenDoor();
            }
        }

        public void OpenDoor()
        {

        }

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
