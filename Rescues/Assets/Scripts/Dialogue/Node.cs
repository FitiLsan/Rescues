using UnityEditor.AnimatedValues;


namespace Rescues
{
    [System.Serializable]
    public class Node
    {
        public AnimBool showFields; // For Editor
        public string text;
        public string leftImage;
        public string rightImage;
        public Answer[] answers;
    }
}
