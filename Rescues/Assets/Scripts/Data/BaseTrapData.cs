using UnityEngine;


namespace Rescues
{
    public abstract class BaseTrapData : ScriptableObject
    {
        public bool IsActive;
        [SerializeField] protected ItemData[] _needItems;
        [SerializeField] protected ItemData _craftingTrap;

        public abstract void ActivateTrap(EnemyData activatorData);
    }
}