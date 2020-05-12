using UnityEngine;


namespace Rescues
{
    public abstract class BaseTrapData : ScriptableObject
    {
        public bool IsActive;     
        public abstract void ActivateTrap(EnemyData activatorData);
    }
}