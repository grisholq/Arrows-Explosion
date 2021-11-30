using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DCFAEngine.CustomEditors
{
    [CustomPropertyDrawer(typeof(KeyValueNamesAttribute), true)]
    [CustomPropertyDrawer(typeof(SerializableDictionary), true)]
    public class SerializableDictionaryDrawer : PropertyDrawer
    {
        private ReorderableList list;

        private string keyName = "";
        private string valueName = "";

        private static KeyValueNamesAttribute lastNames;
        private static int attributeDepth = 0;


        private static int depth = 0;
        public SerializableDictionaryDrawer()
        {
            depth = 0;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            keyName = "Key";
            valueName = "Value";

            if (attribute != null)
            {
                var names = attribute as KeyValueNamesAttribute;
                keyName = names.keyName;
                valueName = names.valueName;
                attributeDepth = 0;
                depth = 0;
                lastNames = names;
            }
            else
            {
                if(depth <= 0)
                {
                    depth = 0;
                    lastNames = null;
                }
                else
                {
                    if (lastNames != null)
                    {
                        attributeDepth++;
                        if (lastNames.keyValueNamesArray.Length > attributeDepth)
                        {
                            keyName = lastNames.keyValueNamesArray[attributeDepth].keyName;
                            valueName = lastNames.keyValueNamesArray[attributeDepth].valueName;
                        }
                    }
                }
            }

            if (list == null)
            {
                SerializedProperty listProperty = property.FindPropertyRelative("pairs");
                list = new ReorderableList(property.serializedObject, listProperty, true, false, true, true)
                {
                    drawElementCallback = DrawElement,
                    elementHeightCallback = GetElementHeight,
                };
            }

            Rect rect1 = position;
            rect1.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect1, property, label);


            if (property.isExpanded)
            {
                Rect rect2 = position;
                rect2.y = rect1.yMax;
                list.DoList(rect2);
            }
        }

        void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

            SerializedProperty keyProperty = element.FindPropertyRelative("Key");
            SerializedProperty valueProperty = element.FindPropertyRelative("Value");

            GUIContent label = new GUIContent();
            label.text = $"Element {index}";

            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginProperty(rect, label, element);

            Rect rect1 = rect;
            rect1.height = EditorGUIUtility.singleLineHeight;
            Rect rect2 = rect;
            rect2.height = EditorGUI.GetPropertyHeight(keyProperty);
            rect2.y = rect1.yMax;
            Rect rect3 = rect;
            rect3.height = EditorGUI.GetPropertyHeight(valueProperty);
            rect3.y = rect2.yMax;

            Color defaultColor = GUI.color;
            GUI.color = Color.gray;
            GUI.Label(rect1, label);
            GUI.color = defaultColor;

            depth++;
            label.text = $"<{keyName}>";
            EditorGUI.PropertyField(rect2, keyProperty, label: label, includeChildren: true);
            label.text = $"<{valueName}>";
            EditorGUI.PropertyField(rect3, valueProperty, label: label, includeChildren: true);
            depth--;

            EditorGUI.EndProperty();
            EditorGUI.EndChangeCheck();
        }

        private float GetElementHeight(int index)
        {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

            SerializedProperty keyProperty = element.FindPropertyRelative("Key");
            SerializedProperty valueProperty = element.FindPropertyRelative("Value");

            float result = EditorGUIUtility.singleLineHeight;
            result += EditorGUI.GetPropertyHeight(keyProperty);
            result += EditorGUI.GetPropertyHeight(valueProperty);
            result += EditorGUIUtility.standardVerticalSpacing;

            return result;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded && list != null)
            {
                return list.GetHeight() + list.footerHeight;
            }

            return EditorGUIUtility.singleLineHeight;
        }
    }

}