using System.Collections.Generic;
using UnityEngine;

namespace Rescues
{
    [CreateAssetMenu(fileName = "LevelsDataArray", menuName = "Data/LevelsDataArray")]
    public class LevelsData : ScriptableObject
    {
        [SerializeField] private string _loadLevelName;
        [SerializeField] private List<string> _levelsNames;
        
        public List<string> LevelsNames => _levelsNames;

        public string LoadLevelName
        {
            get => _loadLevelName;
            set
            {
                if (_levelsNames.Contains(value))
                    _loadLevelName = value;
            }
        }
        
    }
}