using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public class DialogueUI : MonoBehaviour
    {
        #region Fields

        [Space(10)]
        [Header("Npc")]
        public GameObject npcContainer;
        public TextMeshProUGUI npcLabel;
        public TextMeshProUGUI npcText;
        public Color npcLabelColor;
        public Image npcImage;
        public Color npcImageNormalColor;
        public float timeBeforeNextNode;
        public Image npcBackground;

        [Space(10)]
        [Header("Player")]
        public GameObject playerContainer;
        public TextMeshProUGUI playerLabel;
        public PossibleAnswer[] playerTextChoices;
        public Color playerLabelColor;
        public Image playerImage;
        public Color playerImageNormalColor;
        public Image playerBackground;

        [Space(10)]
        [Header("Other")]
        public WritePattern writePattern;
        public GameObject dialogContainer;
        public Image background;
        public NodeSoundContainer nodeSoundContainer;

        #endregion
    }
}
