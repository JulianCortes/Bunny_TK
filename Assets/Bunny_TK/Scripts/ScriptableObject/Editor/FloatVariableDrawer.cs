using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.DataDriven.CustomInspector
{
    [CustomPropertyDrawer(typeof(FloatVariable))]
    public class FloatVariableDrawer : PropertyDrawer
    {

        private static float height = 16f;
        private static float additionalHeight = 18f;
        private static float labelWidth = 30f;
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return Screen.width < 333 ? (height + additionalHeight + additionalHeight) : height + additionalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            Rect contentPosition = EditorGUI.PrefixLabel(position, label);

            //if (position.height > height)
            //{
            //    position.height = height;
            //    EditorGUI.indentLevel += 1;
            //    contentPosition = EditorGUI.IndentedRect(position);
            //    contentPosition.y += additionalHeight;
            //}

            contentPosition.width *= .5f;
            contentPosition.height = height;
            EditorGUI.indentLevel = 0;
            EditorGUIUtility.labelWidth = labelWidth;

            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative<FloatVariable>("initialValue"), new GUIContent("Init"));

            contentPosition.x += contentPosition.width;
            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative<FloatVariable>("runtimeValue"), new GUIContent("Run"));

            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.y += additionalHeight;
            contentPosition.height = height;
            EditorGUIUtility.labelWidth = 0f;
            EditorGUI.indentLevel += 1;

            EditorGUI.PropertyField(contentPosition, property, new GUIContent("Reference"));
            EditorGUI.indentLevel = 0;

            EditorGUI.EndProperty();
        }
    }
}
