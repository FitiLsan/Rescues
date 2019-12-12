using System.Collections.Generic;
using System.Linq;


namespace Rescues
{
    public sealed class GameContext : Contexts
    {
        #region Fields

        public Character Character;
        private readonly SortedList<InteractableObjectType, List<IInteractable>> _onTriggers;
        private readonly List<IInteractable> _interactables;
        
        #endregion


        #region ClassLifeCycles

        public GameContext()
        {
            _onTriggers = new SortedList<InteractableObjectType, List<IInteractable>>();
            _interactables = new List<IInteractable>();
        }
        
        #endregion


        #region Methods

        public void AddTriggers(InteractableObjectType type, ITrigger trigger)
        {
            if (!_interactables.Contains(trigger))
            {
                _interactables.Add(trigger); 
            }
            
            if (_onTriggers.ContainsKey(type))
            {
                _onTriggers[type].Add(trigger);
            }
            else
            {
                _onTriggers.Add(type, new List<IInteractable>
                {
                    trigger
                });
            }
        }
        
        public List<T> GetTriggers<T>(InteractableObjectType type) where T : class, IInteractable
        {
            return _onTriggers.ContainsKey(type) ? _onTriggers[type].Select(trigger => trigger as T).ToList() : null;
        }
        
        public List<IInteractable> GetTriggers(InteractableObjectType type)
        {
            return _onTriggers.ContainsKey(type) ? _onTriggers[type] : null;
        }

        public List<IInteractable> GetListInteractable()
        {
            return _interactables;
        }

        #endregion
    }
}
