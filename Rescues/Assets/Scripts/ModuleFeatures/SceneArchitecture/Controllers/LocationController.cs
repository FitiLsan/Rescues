using DG.Tweening;
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
            string path = Path.Combine(AssetsPathGameObject.Object[GameObjectType.Levels], levelName);
            LocationData[] locationsData = Resources.LoadAll<LocationData>(path);

            foreach (LocationData location in locationsData)
            {
                Location locationInstance = Object.Instantiate(location.LocationPrefab, levelParent);
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

                foreach (Gate gate in location.Gates)
                {
                    gate.OnTransfer += LoadLocation;
                    gate.ThisLocationName = location.LocationName;
                    gate.ThisLevelName = levelName;
                }

                InteractableObjectBehavior[] triggers = location.LocationInstance.transform.GetComponentsInChildren<InteractableObjectBehavior>(true);
                foreach (InteractableObjectBehavior trigger in triggers)
                {
                    Context.AddTriggers(trigger.Type, trigger);
                }

                location.DisableOnScene();
                Locations.Add(location);
            }
        }

        #endregion


        #region Methods

        public void UnloadData()
        {
            foreach (LocationData location in Locations)
            {
                location.Destroy();
            }
        }

        private void LoadLocation(Gate gate, TweenCallback AfterTransfer)
        {
            LevelController.LoadLevel(gate, AfterTransfer);
        }

        #endregion
    }
}