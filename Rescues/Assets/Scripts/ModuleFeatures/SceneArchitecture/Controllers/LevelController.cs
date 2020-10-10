using UnityEngine;
using Object = UnityEngine.Object;


namespace Rescues
{
    public class LevelController : IInitializeController
    {
        private LocationController _activeLevel;
        private LevelsData _levelsData;
        private BootScreen _defaultBootScreen;
        private GameContext _context;
        private Services _services;
        private GameObject _levelParent;
        public LevelController(GameContext context, Services services)
        {
            _context = context;
            _services = services;
        }

        public void Initialize()
        {
            _levelParent = new GameObject("Locations");
            var path = AssetsPathGameObject.Object[GameObjectType.Levels];
            var levelsData = Resources.LoadAll<LevelsData>(path);
            _levelsData = levelsData[0];
            
            _defaultBootScreen = Object.Instantiate((BootScreen)_levelsData.BootScreen, _levelParent.transform);

            
            LoadLevel(_levelsData.GetGate);
        }
        
        public void LoadLevel(IGate gate, IBootScreen bootScreen = null)
        {
            if (_activeLevel == null || _activeLevel.LevelName != gate.GoToLevelName)
            {
                LoadAndUnloadPrefabs(gate.GoToLevelName);
            }

            if (gate.ThisLevelName != gate.GoToLevelName || gate.ThisLocationName != gate.GoToLocationName)
            {
                bootScreen = bootScreen ?? _defaultBootScreen;
                bootScreen.CreateFadeEffect(LoadLevelPart);
            }
            else
            {
                LoadLevelPart();
            }

            void LoadLevelPart()
            {
                var activeLocation = _activeLevel.Locations.Find(l => l.LocationActiveSelf);
                if (activeLocation)
                    activeLocation.CloseLocation();

                var bootLocation = _activeLevel.Locations.Find(l => l.LocationName == gate.GoToLocationName);
                if (!bootLocation)
                    Debug.LogError(activeLocation.LevelName + " не содержит локации с именем " + gate.GoToLocationName);

                var enterGate = bootLocation.Gates.Find(g => g.ThisGateId == gate.GoToGateId);
                if (!enterGate)
                    Debug.LogError("В " + gate.GoToLevelName + " - " + gate.GoToLocationName +
                                   " нет Gate c ID = " + gate.GoToGateId);

                _levelsData.SetLastLevelGate = gate;
                bootLocation.LoadLocation();
                //_services.CameraServices.SetCamera(bootLocation);
            }
        }

        private void LoadAndUnloadPrefabs(string loadLevelName)
        {
            if (_activeLevel != null)
            {
                foreach (var location in _activeLevel.Locations)
                    location.LocationInstance.Destroy();
            }
            
            _activeLevel = new LocationController(this, loadLevelName, _levelParent.transform);
        }

    }
}