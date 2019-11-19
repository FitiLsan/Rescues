using UnityEngine;
using UnityEditor;


namespace Rescues
{
    public class Item
    {
        public string name;
        public int id;
        [Multiline(5)]
        public string discription;
        public string pathIcon;
        public string pathPrefab;
    }
}