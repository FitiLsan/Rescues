using UnityEngine;


namespace Rescues
{
    public abstract class BaseTrapData : ScriptableObject
    {
        [SerializeField] protected ItemData[] _needItems;
        [SerializeField] protected ItemData _craftingTrap;
        [SerializeField] protected bool _isActive;

        public abstract void ActivateTrap(EnemyData activatorData);
    }
}