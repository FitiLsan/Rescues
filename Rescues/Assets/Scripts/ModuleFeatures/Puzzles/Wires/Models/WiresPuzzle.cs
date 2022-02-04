using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Rescues
{
    public class WiresPuzzle : Puzzle
    {
        #region Fileds
        
        private List<MamaConnector> _connectors = new List<MamaConnector>();
        private List<PapaConnector> _papaConnectors = new List<PapaConnector>();
        private Dictionary<int, Vector2> _startPositions = new Dictionary<int, Vector2>();
        private List<WirePoint> _wirePoints = new List<WirePoint>();

        #endregion
        
        
        #region  Propeties

        public List<MamaConnector> Connectors => _connectors;
        public Dictionary<int, Vector2> StartPositions => _startPositions;
        public List<WirePoint> WirePoints => _wirePoints;
        
        #endregion


        #region UnityMethods

        private void Awake()
        {
           var canvas = GetComponent<Canvas>();
           canvas.worldCamera = Camera.main;
           canvas.planeDistance = 45;
           canvas.sortingOrder = 36;
           
           var connectors = gameObject.GetComponentsInChildren<MamaConnector>();
           foreach (var connector in connectors)
           {
               _connectors.Add(connector);
               connector.Connected += CheckComplete;
           }
           
        }

        private void OnEnable()
        {
            if (_startPositions.Count == 0)
            {
                _wirePoints = GetComponentsInChildren<WirePoint>().ToList();
                foreach (var wirePoint in _wirePoints)
                {
                    _startPositions.Add(wirePoint.GetHashCode(), wirePoint.transform.localPosition);
                    if (wirePoint.GetComponent<PapaConnector>())
                    {
                        _papaConnectors.Add(wirePoint.GetComponent<PapaConnector>());
                        _papaConnectors.Last().mamaZonePositions = _connectors;
                    }
                }
            }
            foreach (var papa in _papaConnectors)
            {
                papa.InMamaConnectorZone += CheckMamaConnector;
            }
        }

        public void OnFinish()
        {
            foreach (var connector in _connectors)
            {
                connector.Connected -= CheckComplete;
            }
            foreach (var papa in _papaConnectors)
            {
                papa.InMamaConnectorZone -= CheckMamaConnector;
                papa.mamaZonePositions.Clear();
            }
            _connectors.Clear();
            _papaConnectors.Clear();
        }

        #endregion


        #region Methods

        private void CheckMamaConnector(MamaConnector obj,PapaConnector papaConnector)
        { 
            obj.LockAndUnLockPapaConnector(papaConnector);
        }

        #endregion
        
    }
}