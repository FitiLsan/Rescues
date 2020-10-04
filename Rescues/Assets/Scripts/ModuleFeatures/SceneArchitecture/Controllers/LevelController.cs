using System.Collections.Generic;
using UnityEngine;

namespace Rescues
{
    public class LevelController : IInitializeController
    {
        private List<LocationController> _levels = new List<LocationController>();
        
        public LevelController(GameContext context, Services services)
        {
            
        }
        
        public void Initialize()
        {
            var path = AssetsPathGameObject.Object[GameObjectType.Levels];
            var levelsData = Resources.LoadAll<LevelsData>(path);
Debug.Log(path);
            foreach (var levelName in levelsData[0].LevelsNames)
            {
                var level = new LocationController(levelName);
                _levels.Add(level);
            }
            
        }
        
        
    }
}