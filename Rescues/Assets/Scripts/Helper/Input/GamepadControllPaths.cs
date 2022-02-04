using System.Collections.Generic;

namespace Rescues
{
    public class GamepadControllPaths
    {
        #region Fields

        public static readonly Dictionary<GamepadInputs, string> PathsByInputType = new Dictionary<GamepadInputs, string>()
        {
            { GamepadInputs.Button_South, "buttonSouth"},
            { GamepadInputs.Button_East, "buttonEast"},
            { GamepadInputs.Button_West, "buttonWest"},
            { GamepadInputs.Button_North, "buttonNorth"},
            { GamepadInputs.Shoulder_Left, "leftShoulder"},
            { GamepadInputs.Shoulder_Right, "rightShoulder"},
            { GamepadInputs.Button_Select, "select"},
            { GamepadInputs.Button_Start, "start"},
            { GamepadInputs.Trigger_Left, "leftTrigger"},
            { GamepadInputs.Trigger_Right, "rightTrigger"},

            { GamepadInputs.LeftStick, "leftStick"},
            { GamepadInputs.LeftStick_XAxis, "leftStick/x"},
            { GamepadInputs.LeftStick_YAxis, "leftStick/y"},
            { GamepadInputs.LeftStick_Up, "leftStick/up"},
            { GamepadInputs.LeftStick_Down, "leftStick/down"},
            { GamepadInputs.LeftStick_Left, "leftStick/left"},
            { GamepadInputs.LeftStick_Right, "leftStick/right"},
            { GamepadInputs.LeftStick_Button, "leftStickButton"},

            { GamepadInputs.RightStick, "rightStick"},
            { GamepadInputs.RightStick_XAxis, "rightStick/x"},
            { GamepadInputs.RightStick_YAxis, "rightStick/y"},
            { GamepadInputs.RightStick_Up, "rightStick/up"},
            { GamepadInputs.RightStick_Down, "rightStick/down"},
            { GamepadInputs.RightStick_Left, "rightStick/left"},
            { GamepadInputs.RightStick_Right, "rightStick/right"},
            { GamepadInputs.RightStick_Button, "rightStickButton"},

            { GamepadInputs.DPad, "dpad"},
            { GamepadInputs.DPad_XAxis, "dpad/x"},
            { GamepadInputs.DPad_YAxis, "dpad/y"},
            { GamepadInputs.DPad_Up, "dpad/up"},
            { GamepadInputs.DPad_Down, "dpad/down"},
            { GamepadInputs.DPad_Left, "dpad/left"},
            { GamepadInputs.DPad_Right, "dpad/right"},
        };

        #endregion
    }
}