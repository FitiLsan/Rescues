using System.Collections.Generic;
using System.Linq;


namespace Rescues
{
    public sealed class GameContext : Contexts
    {
        #region Fields
        
        public EnemyBehaviour Enemy;
        public Character Character;
        private readonly SortedList<TriggerObjectType, List<IOnTrigger>> _onTriggers;

        #endregion


        #region ClassLifeCycles

        public GameContext()
        {
            _onTriggers = new SortedList<TriggerObjectType, List<IOnTrigger>>();
        }

        #endregion


        #region Methods

        public void AddTriggers(TriggerObjectType type, IOnTrigger trigger)
        {
            if (_onTriggers.ContainsKey(type))
            {
                _onTriggers[type].Add(trigger);
            }
            else
            {
                _onTriggers.Add(type, new List<IOnTrigger>
                {
                    trigger
                });
            }
        }
        
        public List<T> GetTriggers<T>(TriggerObjectType type) where T : class, IOnTrigger
        {
            return _onTriggers[type].Select(trigger => trigger as T)
                .ToList();
        }

        #endregion
    }
}
