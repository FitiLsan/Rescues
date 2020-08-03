using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public class WiresPuzzle : Puzzle
    {
        #region Fileds

        [SerializeField] private List<MamaConnector> _connectors;

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

            gameObject.SetActive(false);
        }

        #endregion


        #region Methods

        private void CheckComplete()
        {
            var checkCounter = 0;
            for (int i = 0; i < _connectors.Count; i++)
            {
                if (_connectors[i].IsCorrectWire)
                    checkCounter++;
            }
            
            if (checkCounter == _connectors.Count - 1)
                Finish();
        }

        public override void ResetValues()
        {
            //TODO возвращать концы проводов на стартовые позиции
        }

        #endregion
    }
}