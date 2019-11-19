using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


namespace Rescues
{
    public class Inventory : MonoBehaviour 
    {
        List<Item> item;
        public GameObject cellContainer;
        public KeyCode showInventory;
        public KeyCode takeButton;

        public void ShowInventory()
        {
             if (Input.GetKeyDown(showInventory))
            {
                if (cellContainer.activeSelf)
                {
                    cellContainer.SetActive(false);
                }
                else
                {
                    cellContainer.SetActive(true);
                }
            }
        }

        void Start()
        {
            item = new List<Item>();

            cellContainer.SetActive(false);

            for (int i = 0; i < cellContainer.transform.childCount; i++)
            {
                item.Add(new Item());
            }
        }

        void Update()
        {
            ShowInventory();
        }
    }
}
