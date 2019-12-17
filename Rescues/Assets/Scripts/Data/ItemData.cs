using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData")]
    public sealed class ItemData: ScriptableObject
    {
        #region Fields
        
        public string Name;
        [TextArea(5,5)] public string Discription;

        #endregion
    }
}
