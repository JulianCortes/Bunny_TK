using Bunny_TK.EditorUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.DataDriven.CustomInspector
{
    [CustomPropertyDrawer(typeof(Vector3Variable))]
    public class Vector3VariableDrawer : BaseVariableDrawer
    {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (Screen.width < 333)
                return base.GetPropertyHeight(property, label) + EditorHelper.SingleLineHeightWithSpacing * 2f;
            return base.GetPropertyHeight(property, label);
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

            EditorGUI.indentLevel++;
            EditorGUIUtility.labelWidth = 0f;

            //Initial Value Prop
            EditorHelper.ScriptablePropertyField(contentPosition, "Initial Value", property, "initialValue");

            //Moving down
            contentPosition.y += EditorHelper.SingleLineHeightWithSpacing;

            //Adding additional vertical space because Vec3 will double in height by default if Screen.width < 333
            if (Screen.width < 333)
                contentPosition.y += EditorHelper.SingleLineHeightWithSpacing;

            //Runtime Value Prop
            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            EditorHelper.ScriptablePropertyField(contentPosition, "Runtime Value", property, "runtimeValue");
            EditorGUI.EndDisabledGroup();

            //property.serializedObject.Update();
            EditorGUI.indentLevel = 0;
            EditorGUI.EndProperty();
        }
    }


}
