using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace DCFAEngine
{
    public class LayerAttribute : PropertyAttribute
    {
        public LayerAttribute() => isFlags = false;
        public LayerAttribute(bool isFlags)
        {
            this.isFlags = isFlags;
        }
        public bool isFlags;
    }
}

#if UNITY_EDITOR
namespace DCFAEngine.CustomEditors
{
    [CustomPropertyDrawer(typeof(LayerAttribute), true)]
    public class LayerAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                EditorGUI.HelpBox(position, label.text + ": " + property.type + " не верный тип", MessageType.Warning);
                return;
            }

            LayerAttribute layerAttribute = attribute as LayerAttribute;

            Rect labelRect = position;
            labelRect.width = EditorGUIUtility.labelWidth;

            Rect intRect = position;
            intRect.width = labelRect.width + (position.width - labelRect.width) / 2;

            Rect popupRect = position;
            popupRect.width = intRect.width;
            popupRect.x = intRect.xMax;

            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(intRect, property, label);

            if (layerAttribute.isFlags)
            {
                property.intValue = EditorGUI.MaskField(popupRect, property.intValue, InternalEditorUtility.layers);
            }
            else
            {
                property.intValue = EditorGUI.Popup(popupRect, property.intValue, InternalEditorUtility.layers);

                //property.intValue = EditorGUI.Popup(popupRect, property.intValue >> 1, InternalEditorUtility.layers) << 1;
            }

            EditorGUI.EndChangeCheck();
        }
    }
}
#endif