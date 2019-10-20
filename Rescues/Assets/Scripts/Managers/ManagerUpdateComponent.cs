using UnityEngine;
using System.Collections;


namespace Rescues
{
    public class ManagerUpdateComponent : MonoBehaviour
    {

        private ManagerUpdate _mng;

        public void Setup(ManagerUpdate _mng)
        {
            this._mng = _mng;
        }

        private void Update()
        {
            _mng.Tick();
        }

        private void FixedUpdate()
        {
            _mng.TickFixed();
        }

        private void LateUpdate()
        {
            _mng.TickLate();
        }
    }
}
