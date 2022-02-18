using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "StandItemData", menuName = "Data/StandItemData")]
    public class StandItemData : ScriptableObject
    {
        #region Fields

        public Sprite Sprite;
        public ItemData Item;

        #endregion    
    }
}