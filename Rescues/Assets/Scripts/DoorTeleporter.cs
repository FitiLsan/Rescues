using UnityEngine;


namespace Rescues
{
    public class DoorTeleporter : MonoBehaviour
    {
        #region Data
        [SerializeField] private Transform upperDoor;
        [SerializeField] private Transform lowerDoor;
        #endregion


        #region Methods
        public void JumpUp(Transform objectTransfom)
        {
            if (upperDoor != null) objectTransfom.transform.position = upperDoor.position;
        }

        public void JumpDown(Transform objectTransfom)
        {
            if (lowerDoor != null) objectTransfom.transform.position = lowerDoor.position;
        }
        #endregion
    }
}
