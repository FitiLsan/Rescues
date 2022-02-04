using System.Text;
using UnityEngine;

namespace Rescues
{
    public sealed class InputAxis : IInput
    {
        #region Properties

        public InputButton Positive{ get; private set; }
        public InputButton Negative { get; private set; }

        public bool IsReceivingInput 
        {
            get 
            {
                return Positive.IsHeld || Negative.IsHeld;
            }
        }

        public bool IsReleased 
        {
            get 
            {
                return Positive.IsUp && !Negative.IsHeld ||
                    Negative.IsUp && !Positive.IsHeld;
            }
        }

        public float Value 
        { 
            get 
            {
                if (Positive.IsHeld == Negative.IsHeld)
                    return 0f;
                else if (Positive.IsHeld)
                    return 1f;
                else
                    return -1f;
            }
        }

        #endregion


        #region ClassLifeCycles

        public InputAxis(KeyCode positiveKey, KeyCode negativeKey, GamepadInputs gamepadPositive, GamepadInputs gamepadNegative)
        {
            Positive = new InputButton(positiveKey, gamepadPositive);
            Negative = new InputButton(negativeKey, gamepadNegative);
        }

        #endregion


        #region Methods

        public void Rebind(KeyCode positiveKey, KeyCode negativeKey, GamepadInputs gamepadPositive, GamepadInputs gamepadNegative)
        {
            RebindPositive(positiveKey, gamepadPositive);
            RebindNegative(negativeKey, gamepadNegative);
        }

        public void RebindPositive(KeyCode positiveKey, GamepadInputs gamepadPositive)
        {
            Positive.Rebind(positiveKey, gamepadPositive);
        }

        public void RebindNegative(KeyCode negativeKey, GamepadInputs gamepadNegative)
        {
            Negative.Rebind(negativeKey, gamepadNegative);
        }

        #endregion


        #region IInput

        public string GetSaveString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Positive.GetSaveString());
            sb.Append(":");
            sb.Append(Negative.GetSaveString());

            return sb.ToString();
        }

        public void RebindFromString(string rebindString)
        {
            try
            {
                var newBinds = rebindString.Split(':');

                Positive.RebindFromString(newBinds[0]);
                Negative.RebindFromString(newBinds[1]);
            }
            catch (System.Exception e)
            {
                Debug.Log(typeof(InputAxis) + "  |  " + e.Message + "\n" +
                    "Incorrect input string. Axis was not rebound");
            }
        }

        #endregion


        #region IDisposable

        public void Dispose()
        {
            Positive.Dispose();
            Negative.Dispose();
        }

        #endregion
    }
}