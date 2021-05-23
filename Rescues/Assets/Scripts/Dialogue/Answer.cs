namespace Rescues
{
    [System.Serializable]
    public struct Answer
    {
        public DialogueAction action, last;
        public int greater, less, equal, toNode;
        public bool exit;
        public string send, quest, answer;
    }
}
