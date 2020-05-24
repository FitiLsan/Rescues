using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public class FieldOfViewController : MonoBehaviour
    {
        #region PrivateData

        [SerializeField] private bool _isFacingRight;
        private BoxCollider2D _box;
        private float _rayLength = 37f;

        #endregion


        #region UnityMethods
        private void Awake()
        {
            _box = GetComponent<BoxCollider2D>();
        }


        private void Update()
        {
            if (_isFacingRight) Vision(Vector2.right);
            else Vision(Vector2.left);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(TagManager.FOGOFWAR))
            {
                var fog = collision.GetComponent<FogOfWarBehaviour>();
                fog.FogEnter();
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(TagManager.FOGOFWAR))
            {
                var fog = collision.GetComponent<FogOfWarBehaviour>();
                if(!fog.IsPlayerEnter)fog.FogLeft();
            }
        }

        #endregion


        #region Methods

        private void Vision(Vector2 direction)
        {
            RaycastHit2D hit;
            Debug.DrawRay(transform.position, direction * _rayLength);
            hit = Physics2D.Raycast(transform.position, direction, _rayLength, LayerManager.ViewObstacle);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag(TagManager.VIEWOBSTACLE))
                {
                    _box.enabled = false;
                }
            }
            else _box.enabled = true;
        }

        #endregion

    }
}
