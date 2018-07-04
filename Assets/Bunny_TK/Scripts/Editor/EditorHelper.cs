using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.EditorUtils
{
    public static class EditorHelper
    {
        public static float SingleLineHeightWithSpacing
        {
            get
            {
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }
        }

        /// <summary>
        /// Workaround for FindPropertyRelative for ScriptableObjects.
        /// </summary>
        public static SerializedProperty ScriptableFindPropertyRelative(this SerializedProperty serialized, string propertyPath)
        {
            if (serialized.objectReferenceValue == null)
                return null;

            SerializedObject so = new SerializedObject(serialized.objectReferenceValue);
            SerializedProperty prop = so.FindProperty(propertyPath);
            return prop;
        }

        public static void ScriptablePropertyField(Rect contentPosition, string label, SerializedProperty property, string propertyPath)
        {
            SerializedProperty targetProp = property.ScriptableFindPropertyRelative(propertyPath);
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(contentPosition, targetProp, new GUIContent(label));
            if (EditorGUI.EndChangeCheck())
                targetProp.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draw default field for property.
        /// </summary>
        public static void DefaultField(Rect contentPosition, SerializedProperty property)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(contentPosition, property);
            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Creates a Enum Popup style for all items type T in resourcePath. Works only on inspector (not in drawers).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedObject"></param>
        /// <param name="propertyName"></param>
        /// <param name="label"></param>
        /// <param name="resourcePath"></param>
        /// <param name="onAssignFunction">Called on selected.</param>
        public static void ShowEnumResources<T>(SerializedObject serializedObject, string propertyName, string label, string resourcePath, Action<T> onAssignFunction) where T : UnityEngine.Object
        {
            int indexReference = 0;
            List<T> references = Resources.LoadAll<T>("").ToList();
            List<string> names;

            if (references == null) names = new List<string>();
            else names = references.Select(t => t.name).ToList();

            names.Insert(0, "None");

            SerializedProperty property = serializedObject.FindProperty(propertyName);
            if (property.objectReferenceValue == null)
                indexReference = 0;
            else
                indexReference = references.IndexOf(property.objectReferenceValue as T) + 1;

            EditorGUILayout.BeginHorizontal();
            indexReference = EditorGUILayout.Popup(label, indexReference, names.ToArray());

            if (indexReference <= 0) onAssignFunction(null);
            else onAssignFunction(references[indexReference - 1]);

            EditorGUI.BeginDisabledGroup(property.objectReferenceValue == null);
            if (GUILayout.Button("Show", EditorStyles.miniButton, GUILayout.Width(50)))
                EditorGUIUtility.PingObject(property.objectReferenceValue);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();
        }
    }
}
