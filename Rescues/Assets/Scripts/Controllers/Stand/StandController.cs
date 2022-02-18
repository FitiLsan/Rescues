using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public sealed class StandController : IInitializeController, ITearDownController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public StandController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            List<IInteractable> stands = _context.GetTriggers(InteractableObjectType.Stand);
            foreach (IInteractable stand in stands)
            {
                StandBehaviour standBehaviour = stand as StandBehaviour;
                standBehaviour.OnFilterHandler += OnFilterHandler;
                standBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                standBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;
                List<StandItem> itemSlots = standBehaviour.StandItemSlots;
                if (itemSlots == null) continue;
                for (int i = 0; i < itemSlots.Count; i++)
                {
                    if (itemSlots[i] == null) continue;
                    itemSlots[i].OnPointerClickEvent += (item) =>
                    {
                        standBehaviour.StandItemImage.sprite = item.Sprite;
                        standBehaviour.StandItemImage.gameObject.SetActive(true);
                    };
                }
            }
        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            List<IInteractable> stands = _context.GetTriggers(InteractableObjectType.Stand);
            foreach (IInteractable stand in stands)
            {
                StandBehaviour standBehaviour = stand as StandBehaviour;
                standBehaviour.OnFilterHandler -= OnFilterHandler;
                standBehaviour.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                standBehaviour.OnTriggerExitHandler -= OnTriggerExitHandler;
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
        }

        private void OnTriggerExitHandler(ITrigger enteredObject)
        {
            enteredObject.IsInteractable = false;
        }

        #endregion
    }
}
