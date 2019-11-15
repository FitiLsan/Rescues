using UnityEngine;


namespace Rescues
{
    public class InputController : IOnUpdate
    {
        #region Data
        private Vector3 _direction;
        public Character Character;
        #endregion


        #region IOnUpdate
        public void OnUpdate()
        {
            _direction.x = Input.GetAxis("Horizontal");

            if (_direction.x != 0)
            {
                Character.Move(_direction);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (Character.Door != null) Character.Door.JumpUp(Character.transform);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (Character.Door != null) Character.Door.JumpDown(Character.transform);
            }
        }
        #endregion
    }
}