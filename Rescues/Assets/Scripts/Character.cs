using UnityEngine;


namespace Rescues
{
    public class Character : MonoBehaviour
    {
        #region Data       
        [SerializeField] private float _characterSpeed;
        public DoorTeleporter Door;
        #endregion


        #region UnityMethods
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Door"))
            {
                Door = collision.GetComponent<DoorTeleporter>();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Door"))
            {
                if (Door == collision.GetComponent<DoorTeleporter>())
                    Door = null;
            }
        }
        #endregion


        #region Methods
        public void Move(Vector3 direction)
        {
            transform.position += direction * _characterSpeed * Time.deltaTime;
        }
        #endregion
    }
}