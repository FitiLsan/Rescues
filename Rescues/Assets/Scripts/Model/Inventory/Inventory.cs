using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rescues
{
    public class Inventory : MonoBehaviour
    {
        #region Fields

        [SerializeField] List<ItemSlot> ItemSlots;
        [SerializeField] List<ItemRecipe> CraftableItemsList;
        [SerializeField] Image _draggableItem;
        private ItemSlot _draggedSlot;

        #endregion


        #region UnityMethods

        public void Awake()
        {
            if (ItemSlots != null)
            {
                for (int i = 0; i < ItemSlots.Count; i++)
                {
                    ItemSlots[i].OnBeginDragEvent += BeginDrag;
                    ItemSlots[i].OnEndDragEvent += EndDrag;
                    ItemSlots[i].OnDragEvent += Drag;
                    ItemSlots[i].OnDropEvent += Drop;
                }
                CustomDebug.Log(ItemSlots.Count);
            }

        }

        #endregion


        #region Methods

        public bool AddItem(ItemData item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].Item == null)
                {
                    ItemSlots[i].Item = item;
                    return true;
                }
            }
            return false;
        }

        public bool RemoveItem(ItemData item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].Item == item)
                {
                    ItemSlots[i].Item = null;
                    return true;
                }
            }
            return false;
        }

        public bool Contains(ItemData item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].Item == item)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFull()
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].Item == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void BeginDrag(ItemSlot itemSlot)
        {
           if (itemSlot.Item != null)
            {
                _draggedSlot = itemSlot;
                _draggableItem.sprite = itemSlot.Item.Icon;
                _draggableItem.transform.position = Input.mousePosition;
                _draggableItem.enabled = true;
            }
        }

        private void EndDrag(ItemSlot itemSlot)
        {
           _draggedSlot = null;
            _draggableItem.enabled = false;
        }

        private void Drag(ItemSlot itemSlot)
        {
            if (_draggableItem.enabled)
            {
                _draggableItem.transform.position = Input.mousePosition;
            }
        }

        private void Drop(ItemSlot dropItemSlot)
        {
           if (_draggedSlot == null) return;

            ItemData draggedItem = _draggedSlot.Item;
            bool isSomethingCrafted = false;

            foreach (ItemRecipe itemRecipe in CraftableItemsList)
            {
                if (itemRecipe.CanCraft(_draggedSlot.Item, dropItemSlot.Item))
                {
                    if(dropItemSlot.Item.IsDestructuble == false)
                    {
                        _draggedSlot.Item = itemRecipe.Craft(this);
                        isSomethingCrafted = true;
                    }
                    else
                    {
                        dropItemSlot.Item = itemRecipe.Craft(this);
                        isSomethingCrafted = true;
                    }                  
                    break;
                }
            }

            if(isSomethingCrafted == false)
            {
                _draggedSlot.Item = dropItemSlot.Item;
                dropItemSlot.Item = draggedItem;    
            }
        }

        //public ItemData GetItem(ItemData value)
        //{
        //    if (Items.Contains(value))
        //    {
        //        Items.Remove(value);
        //        return value;
        //    }

        //    return null;
        //}

        #endregion
    }
}

