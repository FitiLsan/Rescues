using UnityEngine;


namespace Rescues
{
    public sealed class DoorTeleporterBehaviour : InteractableObjectBehavior
    {
        #region Fields

        [SerializeField] public Transform ExitPoint;
        private ITrigger _triggerImplementation;

        #endregion
    }
}
