using UnityEngine;


namespace Rescues
{
    public sealed class DoorInteractiveBehaviour : InteractableObjectBehavior
    {
        #region Fields

        [SerializeField] public Sprite Sprite;
        private ITrigger _triggerImplementation;

        #endregion
    }
}
