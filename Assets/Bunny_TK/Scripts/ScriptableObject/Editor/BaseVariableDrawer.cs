using Bunny_TK.EditorUtils;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.DataDriven.CustomInspector
{
    //KNOWN ISSUES:
    //  - Due to Unity serialization, drawers for Generics doesn't work, so children of BaseVariable class must be added manually here.
    //  - Due to the default drawer for Vector3 when the window is too narrow, Vector3Variable has it's own custom drawer.

    [CustomPropertyDrawer(typeof(BaseVariableGeneric<>), true)]
    //[CustomPropertyDrawer(typeof(IntVariable), true)]
    //[CustomPropertyDrawer(typeof(FloatVariable), true)]
    //[CustomPropertyDrawer(typeof(StringVariable), true)]
    //[CustomPropertyDrawer(typeof(Vector3Variable), true)]
    [CustomPropertyDrawer(typeof(BaseVariable), true)]
    public class BaseVariableDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //If the property (ScriptableObject) is not assigned, it just draw a 'Default drawer'.
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
            EditorGUI.indentLevel++;
            EditorGUIUtility.labelWidth = 0f;

            //Initial Value Prop
            EditorHelper.ScriptablePropertyField(contentPosition, "Initial Value", property, "initialValue");

            //Moving down
            contentPosition.y += EditorHelper.SingleLineHeightWithSpacing;

            //Runtime Value Prop
            //EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            EditorHelper.ScriptablePropertyField(contentPosition, "Runtime Value", property, "runtimeValue");
            //EditorGUI.EndDisabledGroup();

            //WORKAROUND to force update GUI
            //EditorUtility.SetDirty(property.serializedObject.targetObject);
            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();
        }
    }
}