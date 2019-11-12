using UnityEngine;


namespace Rescues
{
    public class DoorTeleporter : MonoBehaviour
    {
        #region Data
        [SerializeField] Transform upperDoor;
        [SerializeField] Transform lowerDoor;
        #endregion


        #region Methods
        public void JumpUp(Transform go)
        {
            if (upperDoor != null) go.transform.position = upperDoor.position;
        }

        public void JumpDown(Transform go)
        {
            if (lowerDoor != null) go.transform.position = lowerDoor.position;
        }
        #endregion
    }
}
