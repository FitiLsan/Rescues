using UnityEngine;
using Object = UnityEngine.Object;


namespace Rescues
{
    public class LevelController : IInitializeController
    {

        #region Fileds
        
        private LocationController _activeLevel;
        private LevelsData _levelsData;
        private BootScreen _defaultBootScreen;
        private BootScreen _customBootScreen;
        private GameContext _context;
        private Services _services;
        private GameObject _levelParent;

        #endregion

        
        #region Private
        
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
            _defaultBootScreen.name = "DefaultBootScreen";
            _defaultBootScreen.gameObject.SetActive(false);
            LoadLevel(_levelsData.GetGate);
        }
        
        #endregion

        
        #region Methods
        
        public void LoadLevel(IGate gate)
        {
            if (_activeLevel == null || _activeLevel.LevelName != gate.GoToLevelName)
                LoadAndUnloadPrefabs(gate.GoToLevelName);

            var bootLocation = _activeLevel.Locations.Find(l => l.LocationName == gate.GoToLocationName);
            if (!bootLocation)
                Debug.LogError(_activeLevel.LevelName + " не содержит локации с именем " + gate.GoToLocationName);
            
            _customBootScreen = bootLocation.CustomBootScreenInstance;
            
            if (gate.ThisLevelName != gate.GoToLevelName || gate.ThisLocationName != gate.GoToLocationName)
            {
                var bootScreen = _customBootScreen == null ? _defaultBootScreen : _customBootScreen;
                bootScreen.ShowBootScreen(_services, LoadLevelPart);
                
            }

            void LoadLevelPart()
            {
                var activeLocation = _activeLevel.Locations.Find(l => l.LocationActiveSelf);
                if (activeLocation)
                    activeLocation.UnloadLocation();
                
                var enterGate = bootLocation.Gates.Find(g => g.ThisGateId == gate.GoToGateId);
                if (!enterGate)
                    Debug.LogError("В " + gate.GoToLevelName + " - " + gate.GoToLocationName +
                                   " нет Gate c ID = " + gate.GoToGateId);
                
                _levelsData.SetLastLevelGate = gate;
                bootLocation.LoadLocation();
                _context.ActiveLocation = bootLocation;
            }
        }

        private void LoadAndUnloadPrefabs(string loadLevelName)
        {
            if (_activeLevel != null)
            {
                foreach (var location in _activeLevel.Locations)
                    location.Destroy();
            }
            
            _activeLevel = new LocationController(this, loadLevelName, _levelParent.transform);
        }
        
        #endregion

    }
}