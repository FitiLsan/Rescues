#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AnimatedValues;
using System.Text.RegularExpressions;


namespace Rescues
{
    public class DialogueEditor : EditorWindow
    {
        private string path, file, quest, fileName, pathLast;
        private float lineHeight = 30; // высота линий по умолчанию
        private int limitAnswer = 5; // лимит на ответы (зависит от формы диалога)
        private int limitNode = 20; // лимит на количество узлов диалога
        private Vector2 scrollPos;
        private Node[] nodes;

        private string questTip = "Имя проверяемого квеста (для действий ToNodeID и Exit, можно оставить пустым).";
        private string greaterTip = "Значение квеста должно быть больше, чем это.";
        private string lessTip = "Значение квеста должно быть меньше данного.";
        private string equalTip = "Проверить равенство значения с квестом.";
        private string toNodeTip = "Переход на другой узел диалога.";
        private string exitTip = "Завершить диалог.";
        private string sendTip = "Позволяет отправить сообщение менеджеру квестов (оставить пустым если ненужно).";

        [MenuItem("Window/Dialogue Generator...")]
        private static void OpenWindow()
        {
            DialogueEditor editor = GetWindow<DialogueEditor>();
            GUIContent titleContent = new GUIContent("Dialogue Generator");
            editor.minSize = new Vector2(450, 600);
            editor.titleContent = titleContent;
            editor.Init();
        }

        private void Init()
        {
            nodes = new Node[0];
            path = Application.dataPath;
            pathLast = Application.dataPath;
            quest = "";
            fileName = "Default";
        }

        private GUIStyle Style(TextAnchor alignment, FontStyle fontStyle)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = alignment;
            style.fontStyle = fontStyle;
            return style;
        }

        private GUIStyle Style(TextAnchor alignment, Color textColor, float fixedHeight)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = textColor;
            style.alignment = alignment;
            style.fixedHeight = fixedHeight;
            return style;
        }

        private GUIStyle Style(TextAnchor alignment, float fixedHeight, GUIStyle other)
        {
            GUIStyle style = new GUIStyle(other);
            style.alignment = alignment;
            style.fixedHeight = fixedHeight;
            return style;
        }

        private GUIStyle Style(Color textColor, float fixedWidth, GUIStyle other)
        {
            GUIStyle style = new GUIStyle(other);
            style.normal.textColor = textColor;
            style.fixedWidth = fixedWidth;
            return style;
        }

        private GUIStyle Style(Color textColor, GUIStyle other)
        {
            GUIStyle style = new GUIStyle(other);
            style.normal.textColor = textColor;
            return style;
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("Перед началом работы, выберете рабочую папку. Обратите внимание, что рабочую папку можно выбрать только в Resources, текущего проекта. А загружать XML файл локали можно из любой другой директории.", MessageType.Info);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(path, Style(TextAnchor.MiddleCenter, lineHeight, EditorStyles.textArea));
            if (GUILayout.Button("Выбрать путь...", GUILayout.Height(lineHeight), GUILayout.Width(150)))
            {
                path = EditorUtility.OpenFolderPanel("Выбрать путь для файлов...", path, "");
                if (string.IsNullOrEmpty(path))
                {
                    path = Application.dataPath;
                    return;
                }
                Regex regexPath = new Regex(Application.dataPath);
                Regex regexResources = new Regex("Resources");
                if (!regexPath.IsMatch(path) || !regexResources.IsMatch(path))
                {
                    if (EditorUtility.DisplayDialog("Предупреждение...", "Ошибка выбора пути! Необходимо выбирать путь внутри проекта, в папке Resources.", "Ok"))
                    {
                        path = Application.dataPath;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(string.IsNullOrEmpty(file) ? "Загрузить XML файл, для редактирования..." : "Загружен --> " + Path.GetFileNameWithoutExtension(file) + ".xml", Style(TextAnchor.MiddleCenter, lineHeight, EditorStyles.textArea));
            if (GUILayout.Button("Загрузить...", GUILayout.Height(lineHeight), GUILayout.Width(150)))
            {
                file = EditorUtility.OpenFilePanel("Загрузить XML файл...", path, "XML");
                if (string.IsNullOrEmpty(file)) return;
                var extension = Path.GetExtension(file).ToUpper();
                if (extension.CompareTo(".XML") != 0)
                {
                    if (EditorUtility.DisplayDialog("Предупреждение...", "Ошибка! Возможно загрузить только XML файл.", "Ok"))
                    {
                        file = null;
                    }
                }
                else
                {
                    fileName = Path.GetFileNameWithoutExtension(file);
                    LoadFile();
                    return;
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Имя квеста по умолчанию:");
            EditorGUILayout.LabelField("Имя файла сохранения:");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            quest = EditorGUILayout.TextField(quest);
            fileName = EditorGUILayout.TextField(Path.GetFileNameWithoutExtension(string.IsNullOrEmpty(fileName) ? "Default" : fileName));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("- МАССИВ ДИАЛОГА -", Style(TextAnchor.MiddleCenter, FontStyle.Bold));

            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.Width(position.width), GUILayout.Height(position.height - 260));
            GUILayout.BeginVertical(GUILayout.Width(position.width - 15));

            for (int i = 0; i < nodes.Length; i++)
            {
                EditorGUILayout.Separator();
                nodes[i].showFields.target = EditorGUILayout.Foldout(nodes[i].showFields.target, "Узел ID-" + i);

                if (EditorGUILayout.BeginFadeGroup(nodes[i].showFields.faded))
                {
                    EditorGUI.indentLevel++;
                    /*
                    EditorGUILayout.LabelField("Текст НПС:", EditorStyles.boldLabel);
                    EditorGUILayout.BeginHorizontal();
                    nodes[i].text = EditorGUILayout.TextField(nodes[i].text, Style(TextAnchor.MiddleLeft, 20, EditorStyles.textField));
                    EditorGUILayout.EndHorizontal();
                    */

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Левое изображение:", EditorStyles.boldLabel);
                    nodes[i].leftImage = EditorGUILayout.TextField(nodes[i].leftImage, Style(TextAnchor.MiddleLeft, 20, EditorStyles.textField));

                    GUI.backgroundColor = Color.red;
                    if (GUILayout.Button("Удалить узел", Style(Color.white, 100, GUI.skin.button)))
                    {
                        List<Node> list = new List<Node>();

                        for (int j = 0; j < nodes.Length; j++)
                        {
                            if (i != j) list.Add(nodes[j]);
                        }

                        nodes = new Node[list.Count];

                        for (int j = 0; j < nodes.Length; j++)
                        {
                            nodes[j] = list[j];
                        }

                        return;
                    }
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Правое изображение:", EditorStyles.boldLabel);
                    nodes[i].rightImage = EditorGUILayout.TextField(nodes[i].rightImage, Style(TextAnchor.MiddleLeft, 20, EditorStyles.textField));
                    EditorGUILayout.EndHorizontal();

                    if (nodes.Length > 0)
                    {
                        for (int j = 0; j < nodes[i].answers.Length; j++)
                        {
                            EditorGUILayout.Separator();
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Действие: ", Style(TextAnchor.MiddleLeft, 20, EditorStyles.boldLabel));
                            nodes[i].answers[j].action = (DialogueAction)EditorGUILayout.EnumPopup(nodes[i].answers[j].action, Style(TextAnchor.MiddleLeft, 20, EditorStyles.popup));
                            if (GUILayout.Button(new GUIContent("∧", "Выше"), Style(Color.black, 25, GUI.skin.button)))
                            {
                                if (j > 0)
                                {
                                    Answer up = nodes[i].answers[j];
                                    Answer down = nodes[i].answers[j - 1];
                                    nodes[i].answers[j] = down;
                                    nodes[i].answers[j - 1] = up;
                                    return;
                                }
                            }
                            else if (GUILayout.Button(new GUIContent("∨", "Ниже"), Style(Color.black, 25, GUI.skin.button)))
                            {
                                if (j < nodes[i].answers.Length - 1)
                                {
                                    Answer up = nodes[i].answers[j + 1];
                                    Answer down = nodes[i].answers[j];
                                    nodes[i].answers[j + 1] = down;
                                    nodes[i].answers[j] = up;
                                    return;
                                }
                            }
                            GUI.backgroundColor = new Color(1, .5f, 0);
                            if (GUILayout.Button("Удалить", Style(Color.white, 80, GUI.skin.button)))
                            {
                                List<Answer> list = new List<Answer>();

                                for (int t = 0; t < nodes[i].answers.Length; t++)
                                {
                                    if (j != t) list.Add(nodes[i].answers[t]);
                                }

                                nodes[i].answers = new Answer[list.Count];

                                for (int t = 0; t < nodes[i].answers.Length; t++)
                                {
                                    nodes[i].answers[t] = list[t];
                                }

                                return;
                            }
                            GUI.backgroundColor = Color.white;
                            EditorGUILayout.EndHorizontal();
                            string info = string.Empty;
                            EditorGUILayout.Separator();

                            if (nodes[i].answers[j].action != nodes[i].answers[j].last)
                            {
                                nodes[i].answers[j].exit = false;
                                nodes[i].answers[j].equal = 0;
                                nodes[i].answers[j].greater = 0;
                                nodes[i].answers[j].less = 0;
                                nodes[i].answers[j].toNode = 0;
                                nodes[i].answers[j].quest = quest;
                                nodes[i].answers[j].send = string.Empty;
                            }

                            nodes[i].answers[j].equal = Mathf.Abs(nodes[i].answers[j].equal);
                            nodes[i].answers[j].greater = Mathf.Abs(nodes[i].answers[j].greater);
                            nodes[i].answers[j].less = Mathf.Abs(nodes[i].answers[j].less);
                            nodes[i].answers[j].toNode = Mathf.Abs(nodes[i].answers[j].toNode);

                            nodes[i].answers[j].answer = EditorGUILayout.TextField("Фраза: ", nodes[i].answers[j].answer);
                            EditorGUILayout.Separator();

                            switch (nodes[i].answers[j].action)
                            {
                                case DialogueAction.Exit:
                                    info = "Окно диалога будет закрыто.";
                                    nodes[i].answers[j].exit = true;
                                    nodes[i].answers[j].equal = 0;
                                    nodes[i].answers[j].greater = 0;
                                    nodes[i].answers[j].less = 0;
                                    nodes[i].answers[j].toNode = 0;

                                    nodes[i].answers[j].quest = EditorGUILayout.TextField(new GUIContent("Quest: ", questTip), nodes[i].answers[j].quest);
                                    nodes[i].answers[j].send = EditorGUILayout.TextField(new GUIContent("Send: ", sendTip), nodes[i].answers[j].send);
                                    break;

                                case DialogueAction.Equal:
                                    info = "Состояние квеста должно быть строго равно заданному значению Equal.";
                                    nodes[i].answers[j].equal = EditorGUILayout.IntField(new GUIContent("Equal: ", equalTip), nodes[i].answers[j].equal);
                                    nodes[i].answers[j].greater = 0;
                                    nodes[i].answers[j].less = 0;
                                    if (!nodes[i].answers[j].exit) nodes[i].answers[j].toNode = EditorGUILayout.IntField(new GUIContent("Node: ", toNodeTip), nodes[i].answers[j].toNode);
                                    nodes[i].answers[j].quest = EditorGUILayout.TextField(new GUIContent("Quest: ", questTip), nodes[i].answers[j].quest);
                                    nodes[i].answers[j].send = EditorGUILayout.TextField(new GUIContent("Send: ", sendTip), nodes[i].answers[j].send);
                                    nodes[i].answers[j].exit = EditorGUILayout.Toggle(new GUIContent("Exit: ", exitTip), nodes[i].answers[j].exit);

                                    if (string.IsNullOrEmpty(nodes[i].answers[j].quest))
                                    {
                                        nodes[i].answers[j].quest = "TestQuest";
                                    }

                                    break;

                                case DialogueAction.Greater:
                                    info = "Состояние квеста должно быть больше, чем заданное значение Greater.";
                                    nodes[i].answers[j].equal = 0;
                                    nodes[i].answers[j].greater = EditorGUILayout.IntField(new GUIContent("Greater: ", greaterTip), nodes[i].answers[j].greater);
                                    nodes[i].answers[j].less = 0;
                                    if (!nodes[i].answers[j].exit) nodes[i].answers[j].toNode = EditorGUILayout.IntField(new GUIContent("Node: ", toNodeTip), nodes[i].answers[j].toNode);
                                    nodes[i].answers[j].quest = EditorGUILayout.TextField(new GUIContent("Quest: ", questTip), nodes[i].answers[j].quest);
                                    nodes[i].answers[j].send = EditorGUILayout.TextField(new GUIContent("Send: ", sendTip), nodes[i].answers[j].send);
                                    nodes[i].answers[j].exit = EditorGUILayout.Toggle(new GUIContent("Exit: ", exitTip), nodes[i].answers[j].exit);

                                    if (string.IsNullOrEmpty(nodes[i].answers[j].quest))
                                    {
                                        nodes[i].answers[j].quest = "TestQuest";
                                    }

                                    break;

                                case DialogueAction.Less:
                                    info = "Состояние квеста должно быть меньше, чем значение Less.";
                                    nodes[i].answers[j].equal = 0;
                                    nodes[i].answers[j].greater = 0;
                                    nodes[i].answers[j].less = EditorGUILayout.IntField(new GUIContent("Less: ", lessTip), nodes[i].answers[j].less);
                                    if (!nodes[i].answers[j].exit) nodes[i].answers[j].toNode = EditorGUILayout.IntField(new GUIContent("Node: ", toNodeTip), nodes[i].answers[j].toNode);
                                    nodes[i].answers[j].quest = EditorGUILayout.TextField(new GUIContent("Quest: ", questTip), nodes[i].answers[j].quest);
                                    nodes[i].answers[j].send = EditorGUILayout.TextField(new GUIContent("Send: ", sendTip), nodes[i].answers[j].send);
                                    nodes[i].answers[j].exit = EditorGUILayout.Toggle(new GUIContent("Exit: ", exitTip), nodes[i].answers[j].exit);

                                    if (nodes[i].answers[j].less == 0)
                                    {
                                        nodes[i].answers[j].less = 1;
                                    }

                                    if (string.IsNullOrEmpty(nodes[i].answers[j].quest))
                                    {
                                        nodes[i].answers[j].quest = "TestQuest";
                                    }

                                    break;

                                case DialogueAction.GreaterAndLess:
                                    info = "Состояние квеста должно быть больше, чем Greater, но меньше Less.";
                                    nodes[i].answers[j].equal = 0;
                                    nodes[i].answers[j].greater = EditorGUILayout.IntField(new GUIContent("Greater: ", greaterTip), nodes[i].answers[j].greater);
                                    nodes[i].answers[j].less = EditorGUILayout.IntField(new GUIContent("Less: ", lessTip), nodes[i].answers[j].less);
                                    if (!nodes[i].answers[j].exit) nodes[i].answers[j].toNode = EditorGUILayout.IntField(new GUIContent("Node: ", toNodeTip), nodes[i].answers[j].toNode);
                                    nodes[i].answers[j].quest = EditorGUILayout.TextField(new GUIContent("Quest: ", questTip), nodes[i].answers[j].quest);
                                    nodes[i].answers[j].send = EditorGUILayout.TextField(new GUIContent("Send: ", sendTip), nodes[i].answers[j].send);
                                    nodes[i].answers[j].exit = EditorGUILayout.Toggle(new GUIContent("Exit: ", exitTip), nodes[i].answers[j].exit);

                                    if (nodes[i].answers[j].less <= nodes[i].answers[j].greater)
                                    {
                                        nodes[i].answers[j].less = nodes[i].answers[j].greater + 2;
                                    }

                                    if (string.IsNullOrEmpty(nodes[i].answers[j].quest))
                                    {
                                        nodes[i].answers[j].quest = "TestQuest";
                                    }

                                    break;

                                case DialogueAction.ToNodeID:
                                    info = "Переход на другой узел.";
                                    nodes[i].answers[j].equal = 0;
                                    nodes[i].answers[j].greater = 0;
                                    nodes[i].answers[j].less = 0;
                                    nodes[i].answers[j].toNode = EditorGUILayout.IntField(new GUIContent("Node: ", toNodeTip), nodes[i].answers[j].toNode);
                                    nodes[i].answers[j].quest = EditorGUILayout.TextField(new GUIContent("Quest: ", questTip), nodes[i].answers[j].quest);
                                    nodes[i].answers[j].send = EditorGUILayout.TextField(new GUIContent("Send: ", sendTip), nodes[i].answers[j].send);
                                    nodes[i].answers[j].exit = false;
                                    break;
                            }

                            if (string.IsNullOrEmpty(nodes[i].answers[j].quest))
                            {
                                nodes[i].answers[j].send = string.Empty;
                            }

                            nodes[i].answers[j].last = nodes[i].answers[j].action;
                            EditorGUILayout.HelpBox(info, MessageType.Info);
                        }
                    }

                    EditorGUILayout.Separator();

                    GUI.backgroundColor = new Color(0, .5f, 1);
                    if (GUILayout.Button("+ Добавить фразу", Style(Color.white, GUI.skin.button)) && nodes[i].answers.Length < limitAnswer)
                    {
                        Answer[] tmp = nodes[i].answers;
                        nodes[i].answers = new Answer[tmp.Length + 1];

                        for (int j = 0; j < tmp.Length; j++)
                        {
                            nodes[i].answers[j] = tmp[j];
                        }

                        nodes[i].answers[nodes[i].answers.Length - 1].quest = quest;
                        scrollPos.y += 9999;
                    }
                    GUI.backgroundColor = Color.white;

                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.EndFadeGroup();
            }

            EditorGUILayout.Separator();

            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("+ Добавить узел", GUILayout.Height(lineHeight)) && nodes.Length < limitNode)
            {
                Node[] tmp = nodes;
                nodes = new Node[tmp.Length + 1];

                for (int i = 0; i < tmp.Length; i++)
                {
                    nodes[i] = tmp[i];
                }

                nodes[nodes.Length - 1] = new Node();
                nodes[nodes.Length - 1].showFields = new AnimBool(true);
                nodes[nodes.Length - 1].showFields.valueChanged.AddListener(Repaint);
                nodes[nodes.Length - 1].answers = new Answer[0];
                scrollPos.y += 9999;
            }

            GUI.backgroundColor = Color.white;
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Сохранить диалог", GUILayout.Height(lineHeight)))
            {
                Regex regexPath = new Regex(Application.dataPath);
                Regex regexResources = new Regex("Resources");
                if (!regexPath.IsMatch(path) || !regexResources.IsMatch(path))
                {
                    Debug.LogWarning(this + " --> Неверно указан путь...");
                    path = Application.dataPath;
                    return;
                }

                Generate();
            }
            if (GUILayout.Button("Очистить / Сброс", GUILayout.Height(lineHeight)))
            {
                nodes = new Node[0];
                file = null;
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Показать последний сохраненный файл"))
            {
                EditorUtility.RevealInFinder(pathLast);
            }
        }

        private void Generate()
        {
            if (nodes.Length == 0) return;

            XmlNode userNode;
            XmlElement element;
            XmlAttribute attribute;
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(declaration);
            XmlNode rootNode = xmlDoc.CreateElement("dialogue");
            xmlDoc.AppendChild(rootNode);

            for (int i = 0; i < nodes.Length; i++)
            {
                //if (!string.IsNullOrEmpty(nodes[i].text))
                {
                    userNode = xmlDoc.CreateElement("node");
                    attribute = xmlDoc.CreateAttribute("id");
                    attribute.Value = i.ToString();
                    userNode.Attributes.Append(attribute);
                    attribute = xmlDoc.CreateAttribute("text");
                    attribute.Value = nodes[i].text;
                    userNode.Attributes.Append(attribute);
                    attribute = xmlDoc.CreateAttribute("leftImage");
                    attribute.Value = nodes[i].leftImage;
                    userNode.Attributes.Append(attribute);
                    attribute = xmlDoc.CreateAttribute("rightImage");
                    attribute.Value = nodes[i].rightImage;
                    userNode.Attributes.Append(attribute);

                    for (int j = 0; j < nodes[i].answers.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(nodes[i].answers[j].answer))
                        {
                            element = xmlDoc.CreateElement("answer");
                            element.InnerText = nodes[i].answers[j].answer;

                            if (!string.IsNullOrEmpty(nodes[i].answers[j].quest))
                            {
                                attribute = xmlDoc.CreateAttribute("quest");
                                attribute.Value = nodes[i].answers[j].quest;
                                element.Attributes.Append(attribute);
                            }

                            if (!string.IsNullOrEmpty(nodes[i].answers[j].send))
                            {
                                attribute = xmlDoc.CreateAttribute("send");
                                attribute.Value = nodes[i].answers[j].send;
                                element.Attributes.Append(attribute);
                            }

                            switch (nodes[i].answers[j].action)
                            {
                                case DialogueAction.Equal:
                                    attribute = xmlDoc.CreateAttribute("equal");
                                    attribute.Value = nodes[i].answers[j].equal.ToString();
                                    element.Attributes.Append(attribute);
                                    break;

                                case DialogueAction.Greater:
                                    attribute = xmlDoc.CreateAttribute("greater");
                                    attribute.Value = nodes[i].answers[j].greater.ToString();
                                    element.Attributes.Append(attribute);
                                    break;

                                case DialogueAction.Less:
                                    attribute = xmlDoc.CreateAttribute("less");
                                    attribute.Value = nodes[i].answers[j].less.ToString();
                                    element.Attributes.Append(attribute);
                                    break;

                                case DialogueAction.GreaterAndLess:
                                    attribute = xmlDoc.CreateAttribute("less");
                                    attribute.Value = nodes[i].answers[j].less.ToString();
                                    element.Attributes.Append(attribute);
                                    attribute = xmlDoc.CreateAttribute("greater");
                                    attribute.Value = nodes[i].answers[j].greater.ToString();
                                    element.Attributes.Append(attribute);
                                    break;
                            }

                            if (!nodes[i].answers[j].exit)
                            {
                                attribute = xmlDoc.CreateAttribute("toNode");
                                attribute.Value = nodes[i].answers[j].toNode.ToString();
                                element.Attributes.Append(attribute);
                            }
                            else
                            {
                                attribute = xmlDoc.CreateAttribute("exit");
                                attribute.Value = nodes[i].answers[j].exit.ToString();
                                element.Attributes.Append(attribute);
                            }

                            userNode.AppendChild(element);
                        }
                    }

                    rootNode.AppendChild(userNode);
                }
            }

            pathLast = path + "/" + fileName + ".xml";
            xmlDoc.Save(pathLast);
            Debug.Log(this + " --> Cоздан XML файл диалога по адресу: " + pathLast);
        }

        private void LoadFile()
        {
            nodes = new Node[0];
            XmlTextReader reader = new XmlTextReader(file);
            int index = 0;
            while (reader.Read())
            {
                if (reader.IsStartElement("node"))
                {
                    Node[] tmp = nodes;
                    nodes = new Node[tmp.Length + 1];

                    for (int i = 0; i < tmp.Length; i++)
                    {
                        nodes[i] = tmp[i];
                    }

                    nodes[nodes.Length - 1] = new Node();
                    nodes[nodes.Length - 1].showFields = new AnimBool(false);
                    nodes[nodes.Length - 1].showFields.valueChanged.AddListener(Repaint);
                    nodes[nodes.Length - 1].answers = new Answer[0];
                    nodes[nodes.Length - 1].text = reader.GetAttribute("text");
                    nodes[nodes.Length - 1].leftImage = reader.GetAttribute("leftImage");
                    nodes[nodes.Length - 1].rightImage = reader.GetAttribute("rightImage");

                    XmlReader inner = reader.ReadSubtree();
                    while (inner.ReadToFollowing("answer"))
                    {
                        Answer[] tmpAnswer = nodes[index].answers;
                        nodes[index].answers = new Answer[tmpAnswer.Length + 1];

                        for (int j = 0; j < tmpAnswer.Length; j++)
                        {
                            nodes[index].answers[j] = tmpAnswer[j];
                        }

                        string exit = reader.GetAttribute("exit");
                        string greater = reader.GetAttribute("greater");
                        string less = reader.GetAttribute("less");
                        string equal = reader.GetAttribute("equal");
                        string send = reader.GetAttribute("send");
                        string quest = reader.GetAttribute("quest");
                        string toNode = reader.GetAttribute("toNode");

                        if (string.IsNullOrEmpty(greater) && string.IsNullOrEmpty(less) && string.IsNullOrEmpty(equal) && string.IsNullOrEmpty(exit))
                        {
                            nodes[index].answers[nodes[index].answers.Length - 1].action = DialogueAction.ToNodeID;
                            nodes[index].answers[nodes[index].answers.Length - 1].last = DialogueAction.ToNodeID;
                        }
                        else if (string.IsNullOrEmpty(greater) && string.IsNullOrEmpty(less) && !string.IsNullOrEmpty(equal))
                        {
                            nodes[index].answers[nodes[index].answers.Length - 1].action = DialogueAction.Equal;
                            nodes[index].answers[nodes[index].answers.Length - 1].last = DialogueAction.Equal;
                        }
                        else if (string.IsNullOrEmpty(greater) && !string.IsNullOrEmpty(less) && string.IsNullOrEmpty(equal))
                        {
                            nodes[index].answers[nodes[index].answers.Length - 1].action = DialogueAction.Less;
                            nodes[index].answers[nodes[index].answers.Length - 1].last = DialogueAction.Less;
                        }
                        else if (!string.IsNullOrEmpty(greater) && string.IsNullOrEmpty(less) && string.IsNullOrEmpty(equal))
                        {
                            nodes[index].answers[nodes[index].answers.Length - 1].action = DialogueAction.Greater;
                            nodes[index].answers[nodes[index].answers.Length - 1].last = DialogueAction.Greater;
                        }
                        else if (!string.IsNullOrEmpty(greater) && !string.IsNullOrEmpty(less) && string.IsNullOrEmpty(equal))
                        {
                            nodes[index].answers[nodes[index].answers.Length - 1].action = DialogueAction.GreaterAndLess;
                            nodes[index].answers[nodes[index].answers.Length - 1].last = DialogueAction.GreaterAndLess;
                        }
                        else if (!string.IsNullOrEmpty(exit))
                        {
                            nodes[index].answers[nodes[index].answers.Length - 1].action = DialogueAction.Exit;
                            nodes[index].answers[nodes[index].answers.Length - 1].last = DialogueAction.Exit;
                        }

                        nodes[index].answers[nodes[index].answers.Length - 1].answer = reader.ReadString();
                        nodes[index].answers[nodes[index].answers.Length - 1].toNode = GetINT(toNode);
                        nodes[index].answers[nodes[index].answers.Length - 1].exit = GetBOOL(exit);
                        nodes[index].answers[nodes[index].answers.Length - 1].send = send;
                        nodes[index].answers[nodes[index].answers.Length - 1].quest = quest;
                        nodes[index].answers[nodes[index].answers.Length - 1].greater = GetINT(greater);
                        nodes[index].answers[nodes[index].answers.Length - 1].less = GetINT(less);
                        nodes[index].answers[nodes[index].answers.Length - 1].equal = GetINT(equal);
                    }
                    inner.Close();
                    index++;
                }
            }

            reader.Close();
        }

        private int GetINT(string text)
        {
            int value;
            if (int.TryParse(text, out value)) return value;
            return 0;
        }

        private bool GetBOOL(string text)
        {
            bool value;
            if (bool.TryParse(text, out value)) return value;
            return false;
        }
    }
}
#endif