using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "TrapData", menuName = "Data/Traps/TrapData")]
    public sealed class TrapData : ScriptableObject
    {
        [SerializeField] private ItemData[] _needItems;
        [SerializeField] private ItemData _craftingTrap;
    }
}