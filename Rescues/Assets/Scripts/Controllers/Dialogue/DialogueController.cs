using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public class DialogueController : MonoBehaviour
    {
        #region Fields

        public Text nameText;
        public Text dialogueText;
        public GameObject DialogueBox;
        private Queue<string> sentences;

        #endregion


        #region UnityMethods

        void Start()
        {
            sentences = new Queue<string>();
        }

        #endregion


        #region Methods

        public void StartDialogue(DialogueData dialogue)
        {
            DialogueBox.SetActive(true);
            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        #endregion
    }
}