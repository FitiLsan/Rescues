﻿using UnityEngine;


namespace Rescues
{
    public sealed class InitializeInteractableObjectController : IInitializeController
    {
        #region Fields
        
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles
        
        public InitializeInteractableObjectController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController
        
        public void Initialize()
        {
            var triggers = Object.FindObjectsOfType<InteractableObjectBehavior>();
            var buttons = Object.FindObjectsOfType<ButtonBehavior>();

            foreach (var trigger in triggers)
            {
               _context.AddTriggers(trigger.Type, trigger);
            }

            foreach (var button in buttons)
            {
                _context.AddButtons(button.Type, button);
            }
        }


        #endregion
    }
}
