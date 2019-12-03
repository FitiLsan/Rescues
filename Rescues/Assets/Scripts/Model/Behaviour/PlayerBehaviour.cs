using UnityEngine;

namespace Rescues
{
    public sealed class PlayerBehaviour : MonoBehaviour
    {
        #region Properties
        
        public Collider Collider { get; private set; }

        #endregion


        #region UnityMethods
        
        private void Awake()
        {
            Collider = GetComponent<Collider>();
        }

        #endregion
    }
}
