using Bunny_TK.EditorUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.DataDriven.UI.Template
{
    [CustomPropertyDrawer(typeof(BaseTemplateSetting), true)]
    public class TemplateSettingDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight /*+ EditorHelper.SingleLineHeightWithSpacing * additionalLine*/;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var applyProp = property.FindPropertyRelative("apply");
            var valueProp = property.FindPropertyRelative("value");

            Rect contentPosition = position;
            contentPosition.width = EditorGUIUtility.labelWidth - 15f;  //Space for the label of the property for dragging functions (float, int)
            applyProp.boolValue = EditorGUI.ToggleLeft(contentPosition, ObjectNames.NicifyVariableName(property.name), applyProp.boolValue);

            contentPosition.width = position.width - EditorGUIUtility.labelWidth + 15f;

            EditorGUI.BeginDisabledGroup(applyProp.boolValue == false);

            float temp = EditorGUIUtility.labelWidth;
            contentPosition.x = temp;

            EditorGUIUtility.labelWidth = 15f;
            EditorGUI.PropertyField(contentPosition, valueProp, new GUIContent(" "));

            EditorGUIUtility.labelWidth = temp;
            EditorGUI.EndDisabledGroup();

        }
    }
}
