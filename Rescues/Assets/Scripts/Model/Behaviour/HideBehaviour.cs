using UnityEngine;


namespace Rescues
{
    public sealed class HideBehaviour : InteractableObjectBehavior
    {
        #region Fields

        [SerializeField] public Transform ExitPoint;
        private ITrigger _triggerImplementation;

        #endregion
    }
}
