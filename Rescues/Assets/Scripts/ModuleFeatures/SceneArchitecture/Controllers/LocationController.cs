using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Rescues
{
    public class LocationController
    {

        #region Properties
        
        private LevelController LevelController { get; }
        private GameContext Context { get; }
        public List<LocationData> Locations { get; } = new List<LocationData>();
        public string LevelName { get; }

        #endregion

        
        #region Private
        
        public LocationController(LevelController levelController, GameContext context, string levelName, Transform levelParent)
        {
            Context = context;
            LevelName = levelName;
            LevelController = levelController;
            var path = Path.Combine(AssetsPathGameObject.Object[GameObjectType.Levels], levelName);
            var locationsData = Resources.LoadAll<LocationData>(path);

            foreach (var location in locationsData)
            {
                var locationInstance = Object.Instantiate(location.LocationPrefab, levelParent);
                locationInstance.name = location.LocationName;
                location.Gates = locationInstance.transform.GetComponentsInChildren<Gate>().ToList();
                location.LocationInstance = locationInstance;
                location.LevelName = levelName;

                if (location.CustomBootScreenPrefab != null)
                {
                    location.CustomBootScreenInstance = Object.Instantiate(location.CustomBootScreenPrefab, levelParent);
                    location.CustomBootScreenInstance.gameObject.name = "BootScreen - " + location.LocationName;
                    location.CustomBootScreenInstance.gameObject.SetActive(false);
                }

                foreach (var gate in location.Gates)
                {
                    gate.GoAction += LoadLocation;
                    gate.ThisLocationName = location.LocationName;
                    gate.ThisLevelName = levelName;
                }

                var triggers = location.LocationInstance.transform.GetComponentsInChildren<InteractableObjectBehavior>();
                foreach (var trigger in triggers)
                {
                    Context.AddTriggers(trigger.Type, trigger);
                }

                location.DisableOnScene();
                Locations.Add(location);
            }

        }

        #endregion


        #region Methods
        
        private void LoadLocation(Gate gate) => LevelController.LoadLevel(gate);

        public void UnloadData()
        {
            foreach (var location in Locations)
                location.Destroy();
        }

        #endregion
    }
}