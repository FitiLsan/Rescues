using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public sealed class InventoryBehaviour : MonoBehaviour
    {
        #region Fields

        public List<ItemSlot> itemSlots;
        public List<ItemRecipe> craftableItemsList;
        public Image draggableImage;
        public InventoryTooltip inventoryTooltip;

        #endregion


        #region Methods

        public void AddItem(ItemData item)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].Item == null)
                {
                    itemSlots[i].Item = item;
                    break;
                }
            }
        }

        public void RemoveItem(ItemData item)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].Item == item)
                {
                    itemSlots[i].Item = null;
                    break;
                }
            }
        }
        
        public void RemoveItem(string itemID)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].Item?.itemID == itemID)
                {
                    itemSlots[i].Item = null;
                    break;
                }
            }
        }

        public bool Contains(ItemData item)
        {
            bool isContain = false;
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].Item == item)
                {
                    isContain = true;
                    break;
                }
            }
            return isContain;
        }

        public bool IsFull()
        {
            bool isFull = true;
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].Item == null)
                {
                    isFull = false;
                    break;
                }
            }
            return isFull;
        }

        #endregion
    }
}

