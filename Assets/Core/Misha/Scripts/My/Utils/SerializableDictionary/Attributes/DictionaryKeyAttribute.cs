using UnityEditor;
using UnityEngine;

namespace DCFAEngine
{
    public class DictionaryKeyAttribute : PropertyAttribute { }
}

#if UNITY_EDITOR
namespace DCFAEngine.CustomEditors
{
    [CustomPropertyDrawer(typeof(DictionaryKeyAttribute))]
    public class DictionaryKeyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text += " (Key)";
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif
