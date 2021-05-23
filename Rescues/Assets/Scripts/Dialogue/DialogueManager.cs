using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public class DialogueManager
    {
        private static DialogueManager _instance;

        private GameObject _dialogueWindow;
        private DialogueButton[] _buttons;
        private Image _imageLeft;
        private Image _imageRight;

        private int offset = 10;
        private string workFolder = "Data/Dialogue"; // общая папка локалей диалогов в Resources
        private string locale = "Russian";

        private string fileName, lastName;
        private List<Node> node;
        private float curY, height;
        private int id;
        public static bool isActive { get; private set; }

        private string workImageFolder = "Data/Dialogue/Image";

        private DialogueManager()
        {
            GameObject dialogueUI = Resources.Load<GameObject>("Prefabs/UI/DialogueWindow");
            _dialogueWindow = GameObject.Instantiate(dialogueUI);

            //Object[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
            int countButtons = 6;
            Button[] buttons = new Button[countButtons];
            RectTransform[] rects = new RectTransform[countButtons];
            Text[] texts = new Text[countButtons];
            foreach (Transform obj in _dialogueWindow.GetComponentsInChildren<Transform>())
            {
                switch(obj.name)
                {
                    case "DialogueText":
                        buttons[0] = obj.gameObject.GetComponent<Button>();
                        rects[0] = obj.gameObject.GetComponent<RectTransform>();
                        break;
                    case "DialogueTextNPC":
                        texts[0] = obj.gameObject.GetComponent<Text>();
                        break;
                    case "DialogueImageLeft":
                        _imageLeft = obj.gameObject.GetComponent<Image>();
                        break;
                    case "DialogueImageRight":
                        _imageRight = obj.gameObject.GetComponent<Image>();
                        break;
                    case "DialogueButtonAnswer1":
                        buttons[1] = obj.gameObject.GetComponent<Button>();
                        rects[1] = obj.gameObject.GetComponent<RectTransform>();
                        break;
                    case "DialogueButtonAnswer2":
                        buttons[2] = obj.gameObject.GetComponent<Button>();
                        rects[2] = obj.gameObject.GetComponent<RectTransform>();
                        break;
                    case "DialogueButtonAnswer3":
                        buttons[3] = obj.gameObject.GetComponent<Button>();
                        rects[3] = obj.gameObject.GetComponent<RectTransform>();
                        break;
                    case "DialogueButtonAnswer4":
                        buttons[4] = obj.gameObject.GetComponent<Button>();
                        rects[4] = obj.gameObject.GetComponent<RectTransform>();
                        break;
                    case "DialogueButtonAnswer5":
                        buttons[5] = obj.gameObject.GetComponent<Button>();
                        rects[5] = obj.gameObject.GetComponent<RectTransform>();
                        break;
                    case "DialogueTextAnswer1":
                        texts[1] = obj.gameObject.GetComponent<Text>();
                        break;
                    case "DialogueTextAnswer2":
                        texts[2] = obj.gameObject.GetComponent<Text>();
                        break;
                    case "DialogueTextAnswer3":
                        texts[3] = obj.gameObject.GetComponent<Text>();
                        break;
                    case "DialogueTextAnswer4":
                        texts[4] = obj.gameObject.GetComponent<Text>();
                        break;
                    case "DialogueTextAnswer5":
                        texts[5] = obj.gameObject.GetComponent<Text>();
                        break;
                    default:
                        break;
                }
            }

            _buttons = new DialogueButton[countButtons];
            for (int i = 0; i < _buttons.Length; i++)
                _buttons[i] = new DialogueButton(buttons[i], texts[i], rects[i]);
        }

        public static DialogueManager DialogueManagerInstance()
        {
            if (_instance == null)
                _instance = new DialogueManager();

            return _instance;
        }

        // Вызов диалога
        public void CallDialogue(string fileName, Action callback = null)
        {
            this.fileName = fileName;
            Load(callback);
        }

        private void Load(Action callback = null)
        {
            if (lastName == fileName)
            {
                for (int i = 0; i < node.Count; i++)
                    if (BuildDialogue(i, callback))
                        break;
                return;
            }

            node = new List<Node>();

            try
            {
                TextAsset binary = Resources.Load<TextAsset>(workFolder + "/" + locale + "/" + fileName);
                XmlTextReader reader = new XmlTextReader(new StringReader(binary.text));

                int index = 0;
                while (reader.Read())
                {
                    if (reader.IsStartElement("node"))
                    {
                        Node dialogue = new Node();
                        //dialogue.answers = new List<Answer>();
                        List<Answer> prAnswers = new List<Answer>();
                        dialogue.text = reader.GetAttribute("text");
                        dialogue.leftImage = reader.GetAttribute("leftImage");
                        dialogue.rightImage = reader.GetAttribute("rightImage");
                        node.Add(dialogue);
                        XmlReader inner = reader.ReadSubtree();
                        while (inner.ReadToFollowing("answer"))
                        {
                            string exit = reader.GetAttribute("exit");
                            string greater = reader.GetAttribute("greater");
                            string less = reader.GetAttribute("less");
                            string equal = reader.GetAttribute("equal");
                            string send = reader.GetAttribute("send");
                            string quest = reader.GetAttribute("quest");
                            string toNode = reader.GetAttribute("toNode");

                            Answer answer = new Answer();
                            answer.answer = reader.ReadString();
                            answer.toNode = GetINT(toNode);
                            answer.exit = GetBOOL(exit);
                            answer.less = GetINT(less);
                            answer.greater = GetINT(greater);
                            answer.equal = GetINT(equal);
                            answer.quest = quest;
                            answer.send = send;
                            //node[index].answers.Add(answer);
                            prAnswers.Add(answer);
                        }
                        dialogue.answers = prAnswers.ToArray();
                        inner.Close();
                        index++;
                    }
                }

                lastName = fileName;
                reader.Close();
            }
            catch (System.Exception error)
            {
                Debug.Log(this + " ошибка чтения файла диалога: " + fileName + ".xml | Error: " + error.Message);
                CloseWindow();
                lastName = string.Empty;
            }

            for (int i = 0; i < node.Count; i++)
                if (BuildDialogue(i, callback))
                    break;
        }

        private bool BuildDialogue(int current, Action callback = null)
        {
            bool check = false;
            if (current < 0) return check;

            ClearDialogue();

            // Добавление изображений
            if (!string.IsNullOrEmpty(node[current].leftImage))
            {
                _imageLeft.gameObject.SetActive(true);
                _imageLeft.sprite = Resources.Load<Sprite>(workImageFolder + "/" + node[current].leftImage);
            }
            else
                _imageLeft.gameObject.SetActive(false);

            if (!string.IsNullOrEmpty(node[current].rightImage))
            {
                _imageRight.gameObject.SetActive(true);
                _imageRight.sprite = Resources.Load<Sprite>(workImageFolder + "/" + node[current].rightImage);
            }
            else
                _imageRight.gameObject.SetActive(false);
            
            //AddToList(false, 0, node[current].text, null, string.Empty, false);
            if (node[current].answers.Length > 1)
                id = 1;

            for (int i = 0; i < node[current].answers.Length; i++)
            {
                int value = DialogueQuestManager.GetCurrentValue(node[current].answers[i].quest);

                // Фильтр ответов, относительно текущего статуса квеста
                if (value > node[current].answers[i].greater && node[current].answers[i].greater != -1 && node[current].answers[i].less == -1 && node[current].answers[i].equal == -1 ||
                    value == node[current].answers[i].equal && node[current].answers[i].equal != -1 && node[current].answers[i].less == -1 && node[current].answers[i].greater == -1 ||
                    value < node[current].answers[i].less && node[current].answers[i].less != -1 && node[current].answers[i].greater == -1 && node[current].answers[i].equal == -1 ||
                    value < node[current].answers[i].less && node[current].answers[i].less != -1 && value > node[current].answers[i].greater && node[current].answers[i].greater != -1 && node[current].answers[i].equal == -1 ||
                    !string.IsNullOrEmpty(node[current].answers[i].quest) && node[current].answers[i].exit && !string.IsNullOrEmpty(node[current].answers[i].send) || 
                    string.IsNullOrEmpty(node[current].answers[i].quest))
                {
                    AddToList(node[current].answers[i].exit, node[current].answers[i].toNode, node[current].answers[i].answer, node[current].answers[i].send, node[current].answers[i].quest, true, callback);
                    check = true;
                }
            }

            ShowWindow();
            return check;
        }

        int GetINT(string text)
        {
            int value;
            if (int.TryParse(text, out value))
                return value;
            return -1;
        }

        bool GetBOOL(string text)
        {
            bool value;
            if (bool.TryParse(text, out value))
                return value;
            return false;
        }

        private void AddToList(bool exit, int toNode, string text, string send, string quest, bool isActive, Action callback)
        {
            _buttons[id].text.text = text;
            
            //if (id != 0)
            _buttons[id].rect.sizeDelta = new Vector2(_buttons[id].rect.sizeDelta.x, _buttons[id].text.preferredHeight + offset);
            height = _buttons[id].rect.sizeDelta.y;

            _buttons[id].rect.anchoredPosition = new Vector2(0, -height / 2 - curY);
            _buttons[id].button.interactable = isActive;
            _buttons[id].button.gameObject.SetActive(true);

            if (exit)
            {
                SetExitDialogue(_buttons[id].button);
            }
            else
            {
                SetNextNode(_buttons[id].button, toNode, callback);
            }

            if (!string.IsNullOrEmpty(send))
                SetQuestStatus(_buttons[id].button, send, quest, callback);

            id++;

            curY += height;
        }

        private void ClearDialogue()
        {
            id = 0;
            curY = offset;
            foreach (DialogueButton b in _buttons)
            {
                b.text.text = string.Empty;
                b.rect.sizeDelta = new Vector2(b.rect.sizeDelta.x, 0);
                b.rect.anchoredPosition = new Vector2(b.rect.anchoredPosition.x, 0);
                b.button.onClick.RemoveAllListeners();
                b.button.gameObject.SetActive(false);
            }
        }

        private void ShowWindow()
        {
            _dialogueWindow.SetActive(true);
        }

        private void CloseWindow()
        {
            _dialogueWindow.SetActive(false);
        }


        // Событие, для управлением статуса, текущего квеста
        private void SetQuestStatus(Button button, string send, string quest, Action callback)
        {
            string t = quest + "|" + send;
            button.onClick.AddListener(() => QuestStatus(t, callback));
        }

        // Событие, для перенаправления на другой узел диалога
        private void SetNextNode(Button button, int i, Action callback)
        {
            button.onClick.AddListener(() => BuildDialogue(i, callback));
        }

        // Событие, для выхода из диалога
        private void SetExitDialogue(Button button)
        {
            button.onClick.AddListener(() => CloseWindow());
        }

        // Меняем статус квеста
        private void QuestStatus(string s, Action callback)
        {
            string[] t = s.Split(new char[] { '|' });
            DialogueQuestManager.SetQuestStatus(t[0], t[1]);
            if (callback != null)
                callback.Invoke();
        }
    }
}