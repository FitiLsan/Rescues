using System;
using UnityEngine;


namespace Rescues
{
    public class PapaConnector : MonoBehaviour
    {
        private Wire _wire;

        public int Number => _wire.Number;
        public bool IsMoving => _wire.IsMoving;

        private void Awake()
        {
            _wire = GetComponentInParent<Wire>();
            if (_wire == null)
                throw new Exception("This class must be Wire child");
        }
    }
}