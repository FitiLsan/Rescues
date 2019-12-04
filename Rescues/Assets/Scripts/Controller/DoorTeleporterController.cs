using DG.Tweening;
using UnityEngine;


namespace Rescues
{
    public sealed class DoorTeleporterController : IInitializeController, ITearDownController
    {
        #region Fields
        
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles
        
        public DoorTeleporterController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController
        
        public void Initialize()
        {
            var doors = _context.GetTriggers<DoorTeleporterBehaviour>(InteractableObjectType.Door);
            foreach (var trigger in doors)
            {
                trigger.OnTriggerEnterHandler += OnTriggerEnterHandler;
                trigger.OnTriggerExitHandler += OnTriggerExitHandler;
            }
        }

        #endregion


        #region ITearDownController
        
        public void TearDown()
        {
            var doors = _context.GetTriggers<DoorTeleporterBehaviour>(InteractableObjectType.Door);
            foreach (var trigger in doors)
            {
                trigger.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                trigger.OnTriggerExitHandler -= OnTriggerExitHandler;
            }
        }

        #endregion


        #region Methods
        
        private void OnTriggerEnterHandler(ITrigger obj)
        {
            obj.IsInteractable = true;
            var materialColor = obj.GameObject.GetComponent<SpriteRenderer>().color;
            obj.GameObject.GetComponent<SpriteRenderer>().DOColor(new Color(materialColor.r,
                materialColor.g, materialColor.b, 0.5f), 1.0f);
        }

        private void OnTriggerExitHandler(ITrigger obj)
        {
            obj.IsInteractable = false;
            var materialColor = obj.GameObject.GetComponent<SpriteRenderer>().color;
            obj.GameObject.GetComponent<SpriteRenderer>().DOColor(new Color(materialColor.r,
                materialColor.g, materialColor.b, 1.0f), 1.0f);
        }

        #endregion
    }
}
