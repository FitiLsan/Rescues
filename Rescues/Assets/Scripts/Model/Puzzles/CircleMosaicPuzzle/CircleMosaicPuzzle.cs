using System.Collections.Generic;


namespace Rescues
{
    public class CircleMosaicPuzzle : Puzzle
    {
        #region Fileds

        private List<RotatingCircle> _circles = new List<RotatingCircle>();

        #endregion


        #region  Propeties

        public List<RotatingCircle> Circles
        {
            get => _circles;
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            //var obj = Instantiate<RotatingCircle>(new RotatingCircle(), transform);
            //gameObject.
            //var connectors = gameObject.GetComponentsInChildren<MamaConnector>();
            //foreach (var connector in connectors)
            //{
            //    _connectors.Add(connector);
            //    connector.Connected += CheckComplete;
            //}
        }

        #endregion
    }
}