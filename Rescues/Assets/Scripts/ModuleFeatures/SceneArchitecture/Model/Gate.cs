using System;
using UnityEngine;


namespace Rescues
{
    public class Gate : MonoBehaviour, IGate
    {
        #region Fileds

        [NonSerialized] public Action<Gate> GoAction;

        [Header("This Gate")]
        private string _thisLevelName;
        private string _thisLocationName;
        [SerializeField] private int _thisGateId;
        
        [Header("Gate way")]
        [SerializeField] private string _goToLevelName = "Hotel";
        [SerializeField] private string _goToLocationName;
        [SerializeField] private int _goToGateId;
        

        #endregion

        
        #region Private

        public Gate(string levelName, string locationName, int id)
        {
            _thisLevelName = levelName;
            _thisLocationName = locationName;
            _thisGateId = id;
        }

        #endregion
        
        
        #region Properties

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

        #endregion
        

        #region Methods

        [ContextMenu("Go by gate way")]
        public void GoByGateWay()
        {
            GoAction?.Invoke(this);
        }

        #endregion
       
        
    }
}