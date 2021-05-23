using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public class DialogueBehaviour : MonoBehaviour
    {
        private string _quest = "";
        private int _value = 0;
        private List<GameObject> _gameObjectActive = new List<GameObject>();
        private List<GameObject> _gameObjectDeactive = new List<GameObject>();

        public void CallDialogue(string fileName)
        {
            if (_gameObjectActive.Count > 0 || _gameObjectDeactive.Count > 0)
                DialogueManager.DialogueManagerInstance().CallDialogue(fileName, CheckQuestion);
            else
                DialogueManager.DialogueManagerInstance().CallDialogue(fileName);
        }

        public void SetQuestion(string quest)
        {
            _quest = quest;
        }

        public void SetValue(int value)
        {
            _value = value;
        }

        public void AddActiveGameObject(GameObject gameObject)
        {
            _gameObjectActive.Add(gameObject);
        }

        public void AddDeactiveGameObject(GameObject gameObject)
        {
            _gameObjectDeactive.Add(gameObject);
        }

        public void CheckQuestion()
        {
            if (!string.IsNullOrEmpty(_quest))
                if (DialogueQuestManager.GetCurrentValue(_quest) == _value)
                {
                    for (int i = 0; i < _gameObjectActive.Count; i++)
                        _gameObjectActive[i].SetActive(true);
                    for (int i = 0; i < _gameObjectDeactive.Count; i++)
                        _gameObjectDeactive[i].SetActive(false);
                }
            _quest = "";
            _value = 0;
            _gameObjectActive.Clear();
            _gameObjectDeactive.Clear();
        }
    }
}