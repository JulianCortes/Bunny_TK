using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.Utils
{
    //From UnityEditor.PostProcessing
    [CustomPropertyDrawer(typeof(PropertySetAttribute))]
    sealed class PropertySetSetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute = (PropertySetAttribute)base.attribute;

            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label);

            if (EditorGUI.EndChangeCheck())
            {
                attribute.dirty = true;
            }
            else if (attribute.dirty)
            {

                foreach (var t in property.serializedObject.targetObjects)
                {


                    var parent = GetParentObject(property.propertyPath, t);
                    var type = parent.GetType();
                    var info = type.GetProperty(attribute.name);

                    if (info == null)
                        Debug.LogError("Invalid property name \"" + attribute.name + "\"");
                    else
                    {
                        try
                        {
                            info.SetValue(parent, fieldInfo.GetValue(parent), null);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError(ex);
                        }

                    }

                    attribute.dirty = false;
                }
            }
        }

        public static object GetParentObject(string path, object obj)
        {
            var fields = path.Split('.');

            if (fields.Length == 1)
                return obj;

            var info = obj.GetType().GetField(fields[0], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            obj = info.GetValue(obj);

            return GetParentObject(string.Join(".", fields, 1, fields.Length - 1), obj);
        }
    }
}
