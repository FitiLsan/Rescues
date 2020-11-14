using System;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Rescues
{
    public class LevelController : IInitializeController
    {

        #region Fileds
        
        private LocationController _locationController;
        private CurveWayController _curveWayController;
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
            if (_locationController == null || _locationController.LevelName != gate.GoToLevelName)
                LoadAndUnloadPrefabs(gate.GoToLevelName);
            
            var bootLocation = _locationController.Locations.Find(l => l.LocationName == gate.GoToLocationName);
            if (!bootLocation)
                throw new Exception(_locationController.LevelName + " не содержит локации с именем " + gate.GoToLocationName);
            
            _customBootScreen = bootLocation.CustomBootScreenInstance;
            
            if (gate.ThisLevelName != gate.GoToLevelName || gate.ThisLocationName != gate.GoToLocationName)
            {
                var bootScreen = _customBootScreen == null ? _defaultBootScreen : _customBootScreen;
                bootScreen.ShowBootScreen(_services, LoadLevelPart);
            }
            else
            {
               gate.LoadWithTransferTime(LoadLevelPart);
            }

            void LoadLevelPart()
            {
                var activeLocation = _locationController.Locations.Find(l => l.LocationActiveSelf);
                if (activeLocation)
                    activeLocation.DisableOnScene();
                
                var enterGate = bootLocation.Gates.Find(g => g.ThisGateId == gate.GoToGateId);
                if (!enterGate)
                    throw new Exception("В " + gate.GoToLevelName + " - " + gate.GoToLocationName +
                                   " нет Gate c ID = " + gate.GoToGateId);
                
                bootLocation.LoadLocation();
                _levelsData.SetLastLevelGate = gate;

                _context.ActiveLocation = bootLocation;
                _curveWayController = new CurveWayController(bootLocation.LocationInstance.СurveWays);
                var activeCurveWay = _curveWayController.GetCurve(enterGate, WhoCanUseCurve.Character);
                _context.Character.SetPositionAndCurveWay(activeCurveWay);
            }
        }

        private void LoadAndUnloadPrefabs(string loadLevelName)
        {
            _locationController?.UnloadData();
            _curveWayController?.UnloadData();
            _locationController = new LocationController(this, _context, loadLevelName, _levelParent.transform);
        }
        
        #endregion
    }
}