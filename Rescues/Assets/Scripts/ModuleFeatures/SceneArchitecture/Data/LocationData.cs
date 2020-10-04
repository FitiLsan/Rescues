using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "LocationData", menuName = "Data/LocationData")]
    public class LocationData : ScriptableObject
    {

        #region Fileds
        
        [SerializeField] private string _locationName;
        [SerializeField] private bool _loadOnBoot;
        [Space]
        [SerializeField] private GameObject _locationPrefab;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private CameraMode _cameraMode;

        [Header("Заполняется автоматический")] 
        public string LevelName;
        [SerializeField] private GameObject _locationInstance;
        public List<Gate> Gates;

        #endregion
        

        
        #region Properties
        
        public string LocationName => _locationName;
        public GameObject LocationInstance
        {
            get => _locationInstance;
            set
            {
                if (_locationInstance == null)
                    _locationInstance = value;
            }
        }
        public GameObject LocationPrefab => _locationPrefab;
        public Color BackgroundColor => _backgroundColor;
        public CameraMode CameraMode => _cameraMode;
        public bool LoadOnBoot
        {
            get => _loadOnBoot;
            set => _loadOnBoot = value;
        }

        #endregion


        #region UnityMethods
        
        private void OnValidate()
        {
            if (_loadOnBoot)
            {
                var locationDatas = Resources.FindObjectsOfTypeAll<LocationData>().Where(l => l.LoadOnBoot);

                foreach (var data in locationDatas)
                {
                    if (data._locationName != _locationName)
                        data.LoadOnBoot = false;
                }
            }
            
            
        }
        
        #endregion
        
    }
}