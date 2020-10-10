using System;
using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "LocationData", menuName = "Data/LocationData")]
    public class LocationData : ScriptableObject
    {
        #region Fileds

        [NonSerialized] public List<Gate> Gates;
        [NonSerialized] public string LevelName;
        [SerializeField] private string _locationName;
        [SerializeField] private Location _locationPrefab;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private CameraMode _cameraMode;
        [SerializeField] private float _cameraSize;

        public float CameraSize => _cameraSize;

        private Location _locationInstance;

        #endregion
        
        
        #region Properties
        
        public string LocationName => _locationName;

        public bool LocationActiveSelf => _locationInstance.gameObject.activeSelf;
        
        public Location LocationInstance
        {
            get => _locationInstance;
            set
            {
                if (_locationInstance == null)
                    _locationInstance = value;
            }
        }
        public Location LocationPrefab => _locationPrefab;
        public Color BackgroundColor => _backgroundColor;
        public CameraMode CameraMode => _cameraMode;
        
        #endregion


        #region Methods
        
        public void LoadLocation()
        {
            LocationInstance.gameObject.SetActive(true);
                // помещать ГГ на enterGate.transform
        }
        
        public void CloseLocation()
        {
            LocationInstance.gameObject.SetActive(false);
        }
        
        #endregion
        
    }
}