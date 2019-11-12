using UnityEngine;


namespace Rescues
{
    public class InputController : IOnUpdate
    {
        #region Data
        private Vector3 Direction;
        public Character Character;
        #endregion


        #region IOnUpdate
        public void OnUpdate()
        {
            Direction.x = Input.GetAxis("Horizontal");

            if (Direction.x != 0)
            {
                Character._characterDirection.x = Direction.x;
                Character.Move();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (Character._door != null) Character._door.JumpUp(Character.transform);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (Character._door != null) Character._door.JumpDown(Character.transform);
            }
        }
        #endregion
    }
}