using UnityEngine;
using UnityEngine.UI;

namespace Rescues
{
    [CreateAssetMenu(fileName = "StandItemData", menuName = "Data/StandItemData")]
    public class StandItemData : ScriptableObject
    {
        #region Fields

        public Sprite Sprite;
        public bool CanBeTaken;
        public ItemData Item;


        #endregion
       
    }
}