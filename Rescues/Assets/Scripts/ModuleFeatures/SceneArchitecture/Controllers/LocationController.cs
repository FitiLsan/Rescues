using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Rescues
{
    public class LocationController
    {
        private LevelController LevelController { get; }
        public List<LocationData> Locations { get; } = new List<LocationData>();
        public string LevelName { get; }
       

        public LocationController(LevelController levelController, string levelName)
        {
            LevelName = levelName;
            LevelController = levelController;
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
                Locations.Add(location);
            }

        }
        
        public void LoadLocation(Gate gate)
        {
            var activeLocation = Locations.Find(l => l.LocationInstance.activeSelf);
            if (activeLocation)
                activeLocation.LocationInstance.SetActive(false);
        
            LevelController.LoadLevel(gate);
        }
        
            
    }
}