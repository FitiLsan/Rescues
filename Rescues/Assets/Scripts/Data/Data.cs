using System.IO;
using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
    public sealed class Data : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _playerDataPath;
        [SerializeField] private string _itemDataPath;
        private static Data _instance;
        private static PlayerData _playerData;
        private static ItemData _itemData;

        #endregion
        

        #region Properties

        private static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<Data>("Data/" + typeof(Data).Name);
                }

                return _instance;
            }
        }
    

        public static PlayerData PlayerData
        {
            get
            {
                if (_playerData == null)
                {
                    _playerData = Load<PlayerData>("Data/" + Instance._playerDataPath);
                }

                return _playerData;
            }
        }
    

        public static ItemData ItemData
        {
            get
            {
                if (_itemData == null)
                {
                    _itemData = Load<ItemData>("Data/" + Instance._itemDataPath);
                }

                return _itemData;
            }
        }

        #endregion


        #region Methods

        private static T Load<T>(string resourcesPath) where T : Object =>
            Resources.Load<T>(Path.ChangeExtension(resourcesPath, null));
    
        #endregion
    }
}
