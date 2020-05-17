using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public class FieldOfViewController : MonoBehaviour
    {
        private bool _isEnabled = true;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("ViewObstacle"))
            {
                _isEnabled = false;
            }
            else if (collision.gameObject.CompareTag("FogOfWar") && _isEnabled)
            {
                var fog = collision.GetComponent<FogOfWarBehaviour>();
                fog.FogEnter();
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("ViewObstacle"))
            {
                _isEnabled = true;
            }
            else if (collision.gameObject.CompareTag("FogOfWar"))
            {
                var fog = collision.GetComponent<FogOfWarBehaviour>();
                fog.FogLeft();
            }
        }
    }
}
