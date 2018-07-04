using UnityEditor;
using UnityEngine;
using Bunny_TK.EditorUtils;
[CustomPropertyDrawer(typeof(RangeFloat))]
public class RangeFloatDrawer : PropertyDrawer
{
    public static float labelWidth = 30f;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return Screen.width < 333 ? (EditorGUIUtility.singleLineHeight + EditorHelper.SingleLineHeightWithSpacing) : EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        if (position.height > EditorGUIUtility.singleLineHeight)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.y += EditorHelper.SingleLineHeightWithSpacing;
        }

        contentPosition.width *= 0.5f;
        EditorGUIUtility.labelWidth = labelWidth;
        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("min"), new GUIContent("Min"));

        contentPosition.x += contentPosition.width;
        EditorGUIUtility.labelWidth = labelWidth;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("max"), new GUIContent("Max"));
        EditorGUI.EndProperty();
    }
}