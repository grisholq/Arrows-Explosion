using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DCFAEngine.CustomEditors
{
    [CustomPropertyDrawer(typeof(RangedFloat), true)]
    [CustomPropertyDrawer(typeof(RangedInt), true)]
    public class RangedValueDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty minproperty = property.FindPropertyRelative("min");
            SerializedProperty maxproperty = property.FindPropertyRelative("length");

            float defaultlabelWidth = EditorGUIUtility.labelWidth;

            Rect labelRect = position;
            labelRect.width = defaultlabelWidth;
            Rect minRect = position;
            minRect.width = (position.width - labelRect.width) / 2f;
            minRect.x = labelRect.xMax;
            Rect maxRect = position;
            maxRect.width = (position.width - labelRect.width) / 2f;
            maxRect.x = minRect.xMax;


            GUI.Label(labelRect, label);
            EditorGUIUtility.labelWidth = 26f;
            EditorGUI.PropertyField(minRect, minproperty);
            EditorGUIUtility.labelWidth = 46f;
            EditorGUI.PropertyField(maxRect, maxproperty);

            EditorGUIUtility.labelWidth = defaultlabelWidth;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
