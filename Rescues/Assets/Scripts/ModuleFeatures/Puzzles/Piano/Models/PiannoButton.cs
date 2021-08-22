using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public class PiannoButton : MonoBehaviour
    {
        #region Properties

        public Outline Outline { get; set; }
        public AudioSource Audio { get; set; }

        #endregion


        #region UnityMethods

        private void Start()
        {
            Outline = GetComponent<Outline>();
            Audio = GetComponent<AudioSource>();

            Outline.enabled = false;
        }

        #endregion
    }
}
