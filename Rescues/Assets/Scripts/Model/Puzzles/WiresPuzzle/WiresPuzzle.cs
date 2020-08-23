using System.Collections.Generic;


namespace Rescues
{
    public class WiresPuzzle : Puzzle
    {
        #region Fileds
        
        private List<MamaConnector> _connectors = new List<MamaConnector>();
        
        #endregion
        
        
        #region  Propeties

        public List<MamaConnector> Connectors
        {
            get => _connectors;
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            var connectors = gameObject.GetComponentsInChildren<MamaConnector>();
            foreach (var connector in connectors)
            {
                _connectors.Add(connector);
                connector.Connected += CheckComplete;
            }
        }

        #endregion
    }
}