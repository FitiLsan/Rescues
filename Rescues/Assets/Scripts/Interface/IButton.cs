using UnityEngine;

namespace Rescues
{
    public interface IButton
    {
        #region Properties

        InteractableObjectType Type { get; }
        Vector3 Position { get; }
        Collider2D Collider { get; }

        #endregion

        #region Methods
        void Click();
        #endregion

    }
}
