using System.Collections.Generic;


namespace Rescues
{
    public sealed class DialogueCommandValue
    {
        public static readonly Dictionary<DialogueCommands, string> Command = new Dictionary<DialogueCommands, string>()
        {
            { DialogueCommands.CheckItem, "CheckItem"},
            { DialogueCommands.GiveItem, "GiveItem"},
            { DialogueCommands.No,"No" },
            { DialogueCommands.PlayMusic, "PlayMusic" },
            { DialogueCommands.SetNpcName, "SetNpcName" },
            { DialogueCommands.SetNpcSprite, "SetNpcSprite" },
            { DialogueCommands.SetPlayerName, "SetPlayerName" },
            { DialogueCommands.SetPlayerSprite, "SetPlayerSprite" },
            { DialogueCommands.Yes, "Yes" },
            { DialogueCommands.SetStartNode, "SetStartNode" },
            { DialogueCommands.OffNpcContainer, "OffNpcContainer" },
            { DialogueCommands.OnNpcContainer, "OnNpcContainer" },
            { DialogueCommands.ActivateObject, "ActivateObject" },
            { DialogueCommands.ActivateEvent, "ActivateEvent" },
            { DialogueCommands.RemoveItem, "RemoveItem" },
        };
    }
}
