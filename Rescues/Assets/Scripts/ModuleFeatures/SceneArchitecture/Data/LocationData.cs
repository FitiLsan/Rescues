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
        [SerializeField] private GameObject _locationPrefab;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private CameraMode _cameraMode;
        [SerializeField] private int _cameraSize;
        private GameObject _locationInstance;

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
        
        #endregion


        #region Methods
        
        public void LoadLocation()
        {
            LocationInstance.SetActive(true);
                // помещать ГГ на enterGate.transform
        }
        
        #endregion
        
    }
}