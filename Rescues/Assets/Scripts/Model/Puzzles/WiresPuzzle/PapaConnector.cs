using System;
using UnityEngine;


namespace Rescues
{
    public class PapaConnector : MonoBehaviour
    {
        private Wire _wire;
        private bool _isMoving = false;
        
        public int Number => _wire.Number;
        public bool IsMoving => _isMoving;

        private void Awake()
        {
            _wire = GetComponentInParent<Wire>();
            if (_wire == null)
                throw new Exception("This class must be Wire child");
        }
        
        private void OnMouseDown()
        {
            _isMoving = true;
            _wire.SetEndPointRemeber();
            Debug.Log("OnMouseDown()");
        }
        
        private void OnMouseDrag()
        {
            var cursorPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //изощерения с Z из-за непонятности с камерой и расположением объектов на сцене
            cursorPostion.z += 2;
            _wire.MoveWire(cursorPostion);
           
            Debug.Log("OnMouseDrag()");
        }
        
        private void OnMouseUp()
        {
            _isMoving = false;
            Debug.Log(" OnMouseUp()");
        }
    }
}