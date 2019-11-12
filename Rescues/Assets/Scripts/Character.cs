using UnityEngine;


namespace Rescues
{
    public class Character : MonoBehaviour
    {
        #region Data
        public Vector3 _characterDirection;
        [SerializeField] float _characterSpeed;
        public DoorTeleporter _door;
        #endregion


        #region UnityMethods
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Door")
            {
                _door = collision.GetComponent<DoorTeleporter>();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Door")
            {
                if (_door == collision.GetComponent<DoorTeleporter>()) _door = null;
            }
        }
        #endregion


        #region Methods
        public void Move()
        {
            transform.position += _characterDirection * _characterSpeed * Time.deltaTime;
        }
        #endregion
    }
}