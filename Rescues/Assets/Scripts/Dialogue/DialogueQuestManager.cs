using System.Collections.Generic;


namespace Rescues
{
    public class DialogueQuestManager
    {
		// All questions
		private static Dictionary<string, int> _quests = new Dictionary<string, int>();

		public static int GetCurrentValue(string questName)
		{
			int j = 0;

			if (questName != null)
				_quests.TryGetValue(questName, out j);

			return j;
		}

		public static void SetQuestStatus(string quest, string send)
		{
			int j = -1;

			int.TryParse(send, out j);

			if (_quests.ContainsKey(quest))
				_quests[quest] = j;
			else
				_quests.Add(quest, j);
		}
	}
}
