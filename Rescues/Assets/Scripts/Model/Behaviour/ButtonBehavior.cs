using UnityEngine;



namespace Rescues
{
    public class ButtonBehavior : MonoBehaviour, IButton
    {
        #region Variables
        private InteractableObjectType _type = InteractableObjectType.Button;
        #endregion

        #region Properties
        public InteractableObjectType Type { get => _type; }
        public virtual Vector3 Position { get; }
        public virtual Collider2D Collider { get; }
        #endregion

        #region Methods
        public virtual void Click() { }
        #endregion


    }
}

