using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public sealed class PlayerData : ScriptableObject
    {
        public float Speed;
        public Vector3 Position;
    }
}
