using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Bunny_TK.EditorUtils;

namespace Bunny_TK.DataDriven.CustomInspector
{
    [CustomPropertyDrawer(typeof(FloatVariable))]
    public class FloatVariableDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue == null)
                return EditorGUIUtility.singleLineHeight;
            return EditorGUIUtility.singleLineHeight + (EditorHelper.SingleLineHeightWithSpacing * 2);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            Rect contentPosition = position;
            contentPosition.height = EditorGUIUtility.singleLineHeight;

            //'Default'
            EditorGUIUtility.labelWidth = 0f;
            EditorHelper.DefaultField(contentPosition, property);

            //Moving down and resetting labelWidth
            contentPosition.height = EditorGUIUtility.singleLineHeight;
            contentPosition.y += EditorHelper.SingleLineHeightWithSpacing;
            EditorGUI.indentLevel += 1;
            EditorGUIUtility.labelWidth = 0f;

            //Initial Value Prop
            EditorHelper.ScriptablePropertyField(contentPosition, "Initial Value", property, "initialValue");

            //Moving down
            contentPosition.y += EditorHelper.SingleLineHeightWithSpacing;

            //Runtime Value Prop
            EditorHelper.ScriptablePropertyField(contentPosition, "Runtime Value", property, "runtimeValue");

            property.serializedObject.Update();
            EditorGUI.indentLevel = 0;
            EditorGUI.EndProperty();
        }
    }
}
