using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace Bunny_TK.Loot
{
    [CustomEditor(typeof(LootDrop))]
    public class LootDropEditor : Editor
    {
        private LootDrop _Target { get { return target as LootDrop; } }
        private AnimBool amountIsRandom;

        private void OnEnable()
        {
            amountIsRandom = new AnimBool(this.serializedObject.FindProperty("isAmountRandom").boolValue);
            amountIsRandom.valueChanged.AddListener(Repaint);
        }

        public override void OnInspectorGUI()
        {
            //Loot table item
            GUILayout.Space(3);
            ShowEnumResources<LootTableScriptableObject>(this.serializedObject, "lootTableItem", "Loot Table Item", "", _Target.SetLootTableItem);

            //Loot at start
            GUILayout.Space(3);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("isLootsSetAtStart"));
            GUILayout.Space(3);

            //Loot amount
            amountIsRandom.target = EditorGUILayout.Toggle("Is Amount Random", amountIsRandom.target);
            this.serializedObject.FindProperty("isAmountRandom").boolValue = amountIsRandom.target;
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("lootAmount"));
            //Loot amount ref
            if (EditorGUILayout.BeginFadeGroup(amountIsRandom.faded))
                ShowEnumResources<LootTableScriptableObject>(this.serializedObject, "lootTableAmount", "Loot Table Amount", "", _Target.SetLootTableAmount);
            EditorGUILayout.EndFadeGroup();

            //Controls
            //Set random
            GUILayout.Space(3);
            if (GUILayout.Button("Set Random Loot"))
                _Target.InitLoots();
            GUILayout.Space(3);

            //Current Loot
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("loots"), true);

            this.serializedObject.ApplyModifiedProperties();
        }

        private void ShowEnumResources<T>(SerializedObject target, string propertyName, string label, string resourcePath, Action<T> assignFunction) where T : UnityEngine.Object
        {
            int indexReference = 0;
            List<T> references = Resources.LoadAll<T>("").ToList();
            List<string> names;

            if (references == null) names = new List<string>();
            else names = references.Select(t => t.name).ToList();

            names.Insert(0, "None");

            var property = serializedObject.FindProperty(propertyName);
            if (property.objectReferenceValue == null)
            {
                indexReference = 0;
            }
            else
            {
                indexReference = references.IndexOf(property.objectReferenceValue as T) + 1;
            }

            EditorGUILayout.BeginHorizontal();
            indexReference = EditorGUILayout.Popup(label, indexReference, names.ToArray());

            if (indexReference <= 0) assignFunction(null);
            else assignFunction(references[indexReference - 1]);

            EditorGUI.BeginDisabledGroup(property.objectReferenceValue == null);
            if (GUILayout.Button("Show", EditorStyles.miniButton, GUILayout.Width(50)))
                EditorGUIUtility.PingObject(property.objectReferenceValue);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();
        }
    }
}