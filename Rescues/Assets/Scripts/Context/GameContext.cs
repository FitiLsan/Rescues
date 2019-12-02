using System.Collections.Generic;

namespace Rescues
{
    public sealed class GameContext
    {
        public Character Character;
        public readonly HashSet<IOnTrigger> OnTriggers;

        public GameContext()
        {
            OnTriggers = new HashSet<IOnTrigger>();
        }
    }
}
