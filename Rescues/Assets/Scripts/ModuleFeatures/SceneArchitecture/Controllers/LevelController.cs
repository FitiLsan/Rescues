using System;
using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public class LevelController : IInitializeController
    {
        private List<LocationController> _levels = new List<LocationController>();
        private LevelsData _levelsData;
        
        public LevelController(GameContext context, Services services)
        {
            
        }
        
        public void Initialize()
        {
            var path = AssetsPathGameObject.Object[GameObjectType.Levels];
            var levelsData = Resources.LoadAll<LevelsData>(path);
            _levelsData = levelsData[0];
            
            foreach (var levelName in _levelsData.LevelsNames)
            {
                var level = new LocationController(this, levelName);
                _levels.Add(level);
            }
            
            LoadLocation(_levelsData.GetGate);
        }
        
        public void LoadLocation(Gate gate)
        {
            var bootLevel = _levels.Find(l =>
                l.LevelName == gate.GoToLevelName);
            if (bootLevel != null)
                throw new Exception("Нет ни одной локации с совпадением levelName");
            
            var bootLocation = bootLevel.Locations.Find(l => l.LocationName == gate.GoToLocationName);
            if (!bootLocation)
                throw new Exception("Нет ни одной локации с совпадением locationName");
            
            var enterGate = bootLocation.Gates.Find(g => g.ThisGateId == gate.GoToGateId);
            if (!enterGate)
                throw new Exception("В " + gate.GoToLevelName + " - " + gate.GoToLocationName + " нет Gate c ID = " +
                                    gate.GoToGateId);

            bootLocation.LocationInstance.SetActive(true);
            gate.Activated = false;
            enterGate.Activated = true;
            // помещать ГГ на enterGate.transform
        }

       
        
    }
}