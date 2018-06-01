using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangeFloat))]
public class ColorPointDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return Screen.width < 333 ? (16f + 18f) : 16f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        if (position.height > 16f)
        {
            position.height = 16f;
            EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.y += 18f;
        }

        contentPosition.width *= 0.5f;
        EditorGUI.indentLevel = 0;
        EditorGUIUtility.labelWidth = 30f;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("min"), new GUIContent("Min"));

        contentPosition.x += contentPosition.width;
        EditorGUIUtility.labelWidth = 30f;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("max"), new GUIContent("Max"));
        EditorGUI.EndProperty();
    }
}