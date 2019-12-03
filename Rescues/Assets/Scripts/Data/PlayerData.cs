using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public sealed class PlayerData : ScriptableObject
    {
        #region Fields
        
        public float Speed;
        public Vector3 Position;

        #endregion
    }
}
