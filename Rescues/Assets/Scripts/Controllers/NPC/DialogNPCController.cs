using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rescues
{
    public class DialogNPCController : IInitializeController, ITearDownController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public DialogNPCController(GameContext context, Services services)
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
                var NPCBehaviour = trigger as DialogNPCBehaviour;
                NPCBehaviour.OnFilterHandler += OnFilterHandler;
            }
        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            var NPC = _context.GetTriggers(InteractableObjectType.DialogBox);
            foreach (var trigger in NPC)
            {
                var NPCBehaviour = trigger as DialogNPCBehaviour;
                NPCBehaviour.OnFilterHandler -= OnFilterHandler;
            }
        }

        #endregion


        #region Methods

        private bool OnFilterHandler(Collider2D obj)
        {
            return obj.CompareTag(TagManager.PLAYER);
        }


        #endregion
    }
}