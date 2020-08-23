using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace Rescues
{
    [Serializable]
    public struct Rules
    {
        public Vector2 Rule;
    }

    [Serializable]
    public struct CircleMoveScheme
    {
        [Range(0, 360)]
        public int InitialAngle;
        public Sprite Image;
        [ArrayElementTitleAttribute("Rule")]
        public Rules[] Rules;
    }

    [CreateAssetMenu(fileName = "CircleMosaicData", menuName = "Data/CircleMosaicData")]
    public sealed class CircleMosaicData : BasePuzzleData
    {
        #region Fields

        public int Angle = 45;

        [ArrayElementTitleAttribute("Circle")]
        public CircleMoveScheme[] Circles;

        #endregion
    }

    public class ArrayElementTitleAttribute : PropertyAttribute
    {
        public string Varname;
        public ArrayElementTitleAttribute(string ElementTitleVar)
        {
            Varname = ElementTitleVar;
        }
    }

    [CustomPropertyDrawer(typeof(ArrayElementTitleAttribute))]
    public class ArrayElementTitleDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        protected virtual ArrayElementTitleAttribute Atribute
        {
            get { return (ArrayElementTitleAttribute)attribute; }
        }

        SerializedProperty TitleNameProp;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Debug.Log(property.propertyPath);
            var lastSquareBracketIndex = property.propertyPath.LastIndexOf('[');
            int pos = int.Parse(property.propertyPath[lastSquareBracketIndex + 1].ToString());
            EditorGUI.PropertyField(position, property, new GUIContent(Atribute.Varname + $" {pos + 1}"), true);
        }
    }
}
