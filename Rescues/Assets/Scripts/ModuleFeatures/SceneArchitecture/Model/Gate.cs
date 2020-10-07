using System;
using UnityEngine;


namespace Rescues
{
    public class Gate : MonoBehaviour
    {
        public bool Activated;
        [NonSerialized] public Action<Gate> GoAction;
        public bool BootOnLoad;
        
        [Header("Этот Gate")]
         private string _thisLevelName;
        private string _thisLocationName;
        [SerializeField] private int _thisGateId;
        
        [Header("Куда ведет")]
        [SerializeField] private string _goToLevelName = "Hotel";
        [SerializeField] private string _goToLocationName;
        [SerializeField] private int _goToGateId;
        
        public string ThisLevelName
        {
            get => _thisLevelName;
            set => _thisLevelName = value;
        }

        public string ThisLocationName
        {
            get => _thisLocationName;
            set => _thisLocationName = value;
        }

        public int ThisGateId => _thisGateId;
        public string GoToLevelName => _goToLevelName;
        public string GoToLocationName => _goToLocationName;
        public int GoToGateId => _goToGateId;

        public Gate(string levelName, string locationName, int id)
        {
            _thisLevelName = levelName;
            _thisLocationName = locationName;
            _thisGateId = id;
        }
        
        
        [ContextMenu("Go by gate way")]
        public void GoByGateWay()
        {
            GoAction?.Invoke(this);
        }
        
    }
}