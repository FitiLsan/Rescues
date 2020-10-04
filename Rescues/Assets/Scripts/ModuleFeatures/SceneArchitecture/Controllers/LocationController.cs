using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Rescues
{
    public class LocationController
    {
        private List<LocationData> _locations = new List<LocationData>();
        
        public List<LocationData> Locations => _locations;

        public LocationController(string levelName)
        {
            var levelParent = new GameObject(levelName);
            var path = AssetsPathGameObject.Object[GameObjectType.Levels] + "/" + levelName;
            var locationsData = Resources.LoadAll<LocationData>(path);

            foreach (var location in locationsData)
            {
                var locationInstance = Object.Instantiate(location.LocationPrefab, levelParent.transform);
                locationInstance.name = location.LocationName;
                location.Gates = locationInstance.transform.GetComponentsInChildren<Gate>().ToList();
                location.LocationInstance = locationInstance;
                location.LevelName = levelName;

                foreach (var gate in location.Gates)
                {
                    gate.GoAction += LoadLocation;
                    gate.ThisLocationName = location.LocationName;
                    gate.ThisLevelName = levelName;
                }
                location.LocationInstance.SetActive(false);
                _locations.Add(location);
            }

            LoadLocationOnBoot();
        }

        public void LoadLocation(Gate gate)
        {
            DeactiveCurrentLocation();
            var bootLocation = Locations.Find(l =>
                l.LevelName == gate.GoToLevelName && l.LocationName == gate.GoToLocationName);
            if (!bootLocation)
                throw new Exception("Нет ни одной локации с совпадением locationName и levelName");

            bootLocation.LocationInstance.SetActive(true);
            var enterGate = bootLocation.Gates.Find(g => g.ThisGateId == gate.GoToGateId);
            if (!enterGate)
                throw new Exception("В " + gate.GoToLevelName + " - " + gate.GoToLocationName + " нет Gate c ID = " +
                                    gate.GoToGateId);

            
            gate.Activated = false;
            enterGate.Activated = true;
            // помещать ГГ на enterGate.transform
        }

        private void LoadLocationOnBoot()
        {
            DeactiveCurrentLocation();
            var bootLocation = Locations.Find(l => l.LoadOnBoot);
            if (bootLocation)
            {
               var gate = bootLocation.Gates.First(g => g.BootOnLoad);
               gate.Activated = true;
                bootLocation.LocationInstance.SetActive(true);
                
                // помещать ГГ на gate.transform
            }
                
        }
        
        private void DeactiveCurrentLocation()
        {
            var activeLocation = Locations.Find(l => l.LocationInstance.activeSelf);
            if (activeLocation)
                activeLocation.LocationInstance.SetActive(false);
        }
        
    }
}