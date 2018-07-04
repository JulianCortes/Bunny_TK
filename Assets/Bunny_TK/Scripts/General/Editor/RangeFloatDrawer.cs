using UnityEditor;
using UnityEngine;
using System.Reflection;
using Bunny_TK.EditorUtils;

[CustomPropertyDrawer(typeof(RangeFloatMinMaxAttribute))]
[CustomPropertyDrawer(typeof(RangeFloat))]
public class RangeFloatDrawer : PropertyDrawer
{
    public static float labelWidth = 30f;
    public static float sliderWidthNorm = .75f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return Screen.width < 333 ? (EditorGUIUtility.singleLineHeight + EditorHelper.SingleLineHeightWithSpacing) : EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty minProp = property.FindPropertyRelative("min");
        SerializedProperty maxProp = property.FindPropertyRelative("max");
        RangeFloatMinMaxAttribute attribute = this.attribute as RangeFloatMinMaxAttribute;
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        label = EditorGUI.BeginProperty(position, label, property);

        //If with attribute, draw a MinMax Slider
        if (attribute == null)
        {
            //if window is too narrow draw properties in a indented second line.
            if (position.height > EditorGUIUtility.singleLineHeight)
            {
                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.indentLevel++;
                contentPosition = EditorGUI.IndentedRect(position);
                contentPosition.y += EditorHelper.SingleLineHeightWithSpacing;
            }

            contentPosition.width *= .5f;
            contentPosition.width -= 1f; //For right spacing.

            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUI.indentLevel = 0;
            EditorGUI.PropertyField(contentPosition, minProp);

            contentPosition.x += contentPosition.width + 2f;
            EditorGUI.PropertyField(contentPosition, maxProp);
        }
        else
        {
            //Saving values beacuse it's not possibile to reference a SerializedProperty in MinMaxSlider.
            float minVal = minProp.floatValue;
            float maxVal = maxProp.floatValue;
            float labelWidth = contentPosition.width;

            //If Window is wide enough draw slider and values in a single line.
            if (position.height <= EditorGUIUtility.singleLineHeight)
                contentPosition.width *= sliderWidthNorm;
            contentPosition.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.MinMaxSlider(contentPosition, ref minVal, ref maxVal, attribute.minLimit, attribute.maxLimit);

            minProp.floatValue = minVal;
            maxProp.floatValue = maxVal;

            if (position.height > EditorGUIUtility.singleLineHeight)
            {
                //If drawing in 2 lines.
                contentPosition.height = EditorGUIUtility.singleLineHeight;
                contentPosition.width = (position.width - EditorGUIUtility.labelWidth - 2f) / 2f;

                //Moving left, somwhow labelwidth is missing 14px from default.
                contentPosition.x = EditorGUIUtility.labelWidth + 14f;
                contentPosition.y += EditorHelper.SingleLineHeightWithSpacing;
            }
            else
            {
                //If drawing in a single line, moving left + spacing.
                contentPosition.x += contentPosition.width + 2f;

                //Equally dived remaing space for min max field.
                contentPosition.width = (labelWidth * (1f - sliderWidthNorm) / 2f) - 2f;
            }

            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("min"), GUIContent.none);

            contentPosition.x += contentPosition.width + 2f;

            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("max"), GUIContent.none);
        }
        EditorGUI.EndProperty();
    }
}