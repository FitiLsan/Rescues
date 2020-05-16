using UnityEngine;

namespace Rescues
{
    [CreateAssetMenu(fileName = "ItemRecipe", menuName = "Data/ItemRecipe")]
    public sealed class ItemRecipe : ScriptableObject
    {
        #region Fields

        public ItemData ItemA;
        public ItemData ItemB;
        public ItemData ItemResult;

        #endregion       

        public bool CanCraft(ItemData itemA, ItemData itemB)
        {
            if (ItemA == itemA && ItemB == itemB)
            {
                return true;
            }
            else if(ItemA == itemB && ItemB == itemA)
            {
                return true;
            }
            return false;
        }


        public ItemData Craft(Inventory inventory)
        {
            if(ItemA.IsDestructuble) inventory.RemoveItem(ItemA);
            if(ItemB.IsDestructuble) inventory.RemoveItem(ItemB);                      
            return ItemResult;
        }
    }
}
