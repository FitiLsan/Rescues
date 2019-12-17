using System.Collections.Generic;


namespace Rescues
{
    public readonly struct Inventory
    {
        #region Fields

        public readonly List<ItemData> Items;
        private readonly int _size;

        #endregion


        #region ClassLifeCycle

        public Inventory(int size)
        {
            Items = new List<ItemData>(size);
            _size = size;
        }

        #endregion


        #region Methods

        public bool AddItem(ItemData value)
        {
            if (Items.Count < _size)
            {
                Items.Add(value);
                return true;
            }

            return false;
        }

        public ItemData GetItem(ItemData value)
        {
            if(Items.Contains(value))
            {
                Items.Remove(value);
                return value;
            }

            return null;
        }

        #endregion
    }
}
