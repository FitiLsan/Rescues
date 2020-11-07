using UnityEngine;


namespace Rescues
{
    public class DialogueTriggerController : MonoBehaviour
    {
        #region Fields

        public DialogueData _dialogue;
        private readonly GameContext _context;

        #endregion


        #region Properities

        public Collider Collider{get; private set;}

        #endregion


        #region Methods

        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueController>().StartDialogue(_dialogue);
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