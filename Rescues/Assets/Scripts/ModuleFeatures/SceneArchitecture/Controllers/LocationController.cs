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
       

        public LocationController(LevelController levelController, string levelName, Transform levelParent)
        {
            LevelName = levelName;
            LevelController = levelController;
            var path = AssetsPathGameObject.Object[GameObjectType.Levels] + "/" + levelName;
            var locationsData = Resources.LoadAll<LocationData>(path);

            foreach (var location in locationsData)
            {
                var locationInstance = Object.Instantiate(location.LocationPrefab, levelParent);
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

                location.CloseLocation();
                Locations.Add(location);
            }

        }

        private void LoadLocation(Gate gate) => LevelController.LoadLevel(gate);
        
        
            
    }
}