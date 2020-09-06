using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Rescues
{
    public class Dialog : MonoBehaviour
    {
        #region Fields

        [SerializeField] Text text;

        #endregion


        #region UnityMethods

        protected virtual void OnValidate()
        {
            if (text == null)
            {
                text= GetComponent<Text>();
            }
        }

        #endregion
    }
}