using System;
using UnityEngine;

//TODO Разобраться почему этот СО слетает при загрузке
namespace Rescues
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Data/LevelsDataArray")]
    public class LevelsData : ScriptableObject
    {

        #region Fileds
        
        [Header("Default load level")]
        [SerializeField] private string _defaultLevelName;
        [SerializeField] private string _defaultLocationName;
        [SerializeField] private int _defaultGateId;
        [Header("Last Level")]
        [SerializeField] private bool _loadFromLastLevel;
        [SerializeField] private string _lastLevelName;
        [SerializeField] private string _lastLocationName;
        [SerializeField] private int _lastGateId;
        [Header("Default boot screen")]
        [SerializeField] private BootScreen _bootScreen;

        #endregion

        
        #region Properties

        public IBootScreen BootScreen => _bootScreen;
        private IGate DefaultLoadGate => GateDataMock.GetMock(_defaultLevelName, _defaultLocationName, _defaultGateId);
        public IGate GetGate => _loadFromLastLevel ? LastLoadGate : DefaultLoadGate;
        private IGate LastLoadGate
        {
            get
            {
                if (_lastLevelName == String.Empty || _lastLocationName == String.Empty || _lastGateId == 0)
                    return DefaultLoadGate;
                return GateDataMock.GetMock(_lastLevelName, _lastLocationName, _lastGateId);
            }
        }

        public IGate SetLastLevelGate
        {
            set
            {
                _lastLevelName = value.GoToLevelName;
                _lastLocationName = value.GoToLocationName;
                _lastGateId = value.GoToGateId;
            }
        }
        
        #endregion
    }
}