using UnityEngine;


namespace Rescues
{
    public class DialogueTrigger : MonoBehaviour
    {
        #region Fields

        public DialogueData dialogue;

        #endregion


        #region Properities

        public Collider Collider{get; private set;}

        #endregion


        #region Methods

        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueController>().StartDialogue(dialogue);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                TriggerDialogue();
            }
        }

        #endregion
    }
}