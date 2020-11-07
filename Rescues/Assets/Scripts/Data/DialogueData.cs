using UnityEngine;

namespace Rescues
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "Data/DialogueData")]
    public sealed class DialogueData : ScriptableObject
    {
        public string name;

        [TextArea(3, 10)]
        public string[] sentences;
    }
}