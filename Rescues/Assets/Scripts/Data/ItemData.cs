using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData")]
    public sealed class ItemData: ScriptableObject
    {
        #region Fields
        
        [TextArea(5,5)]
        public string di;

        #endregion
    }
}
