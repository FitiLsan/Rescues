using UnityEngine;

namespace Rescues
{
    public sealed class PlayerBehaviour : MonoBehaviour
    {
        public Collider Collider { get; private set; }
        private void Awake()
        {
            Collider = GetComponent<Collider>();
        }
    }
}
