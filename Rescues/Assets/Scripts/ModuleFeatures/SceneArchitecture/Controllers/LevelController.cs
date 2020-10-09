using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;


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
            
            //Load bootscreen object
            var bootScreenPath = AssetsPathGameObject.Object[GameObjectType.UI] + "/" + _levelsData.BootScreen.name;
            var bootScreen = Resources.Load<BootScreen>(bootScreenPath);
            var bootScreenInstance = Object.Instantiate(bootScreen);
            var spriteRenderer = bootScreenInstance.gameObject.GetComponent<SpriteRenderer>();
            _levelsData.BootScreen = spriteRenderer;
            bootScreenInstance.gameObject.SetActive(false);
            
            
            foreach (var levelName in _levelsData.LevelsNames)
            {
                var level = new LocationController(this, levelName);
                _levels.Add(level);
            }
            
            LoadLevel(_levelsData.GetGate);
        }
        
        public void LoadLevel(IGate gate)
        {
            var bootLevel = _levels.Find(l =>
                l.LevelName == gate.GoToLevelName);
            if (bootLevel == null)
                throw new Exception("Нет ни одного уровня с совпадением " + gate.GoToLevelName);
            
            var bootLocation = bootLevel.Locations.Find(l => l.LocationName == gate.GoToLocationName);
            if (!bootLocation)
                throw new Exception("Нет ни одной локации с совпадением " + gate.GoToLocationName);
            
            var enterGate = bootLocation.Gates.Find(g => g.ThisGateId == gate.GoToGateId);
            if (!enterGate)
                throw new Exception("В " + gate.GoToLevelName + " - " + gate.GoToLocationName + " нет Gate c ID = " +
                                    gate.GoToGateId);


            _levelsData.SetLastLevelGate = gate;
          
            
            
            bootLocation.LoadLocation();
                
               
            gate.Activated = false;
            enterGate.Activated = true;
            
        }

       
        
    }
}