using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Rescues
{
    public class LocationController : IExecuteController
    {

        private LocationData _activeLocation;

        #region Properties
        private LevelController LevelController { get; }
        private GameContext Context { get; }
        public List<LocationData> Locations { get; } = new List<LocationData>();

        public LocationData ActiveLocation
        {
            get => _activeLocation;
            set
            {
                Context.ActiveLocation = value;
                _activeLocation = value;
            }
        }
        
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
                    location.CustomBootScreenInstance.gameObject.name = "BootScreen" + location.LocationName;
                    location.CustomBootScreenInstance.gameObject.SetActive(false);
                }

                foreach (var gate in location.Gates)
                {
                    gate.GoAction += LoadLocation;
                    gate.ThisLocationName = location.LocationName;
                    gate.ThisLevelName = levelName;
                }

                location.UnloadLocation();
                Locations.Add(location);
            }

        }

        #endregion
        
        private void LoadLocation(Gate gate) => LevelController.LoadLevel(gate);

        public void SetCharacterScale()
        {
           //var scale = Context.Character.Transform.localScale;
           var position = Context.Character.Transform.position;
           var scale = Vector3.one * ActiveLocation.LocationInstance.ScalePoint.GetScale(position);
           Context.Character.Transform.localScale = scale;
        }


        public void Execute()
        {
            SetCharacterScale();
        }
    }
}