using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LootTableScriptableObject))]
public class LootTableEditor : Editor
{
    private bool tableIsLoaded;
    private const float controlsWidth = 20f;
    private const float percentWidth = 70f;

    private LootTableScriptableObject _lootTable { get { return target as LootTableScriptableObject; } }

    public override void OnInspectorGUI()
    {

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Space(3);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("tableName"));
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
        tableIsLoaded = _lootTable.loots != null && _lootTable.loots.Count > 0;

        if (tableIsLoaded)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                    EditorGUILayout.TextField("Name", EditorStyles.boldLabel);
                    EditorGUILayout.TextField("Additional", EditorStyles.boldLabel);
                    EditorGUILayout.TextField("GameObject", EditorStyles.boldLabel);
                    EditorGUILayout.TextField("Weight", EditorStyles.boldLabel);
                    EditorGUILayout.TextField("%", EditorStyles.boldLabel, GUILayout.Width(percentWidth));
                    EditorGUILayout.TextField("", EditorStyles.boldLabel, GUILayout.Width(controlsWidth));
                GUILayout.EndHorizontal();

            for (int i = 0; i < _lootTable.loots.Count; i++)
            {
                CurrentLootGUI(_lootTable.loots[i]);
                _lootTable.UpdatePercentages();
            }
            GUILayout.EndVertical();
            EditorGUILayout.EndVertical();

        }
        GUILayout.Space(3);
        if (GUILayout.Button("Add"))
        {
            if (_lootTable.loots == null)
                _lootTable.loots = new List<Loot>();
            _lootTable.loots.Add(new Loot());
        }

        if (GUILayout.Button("Clear"))
            _lootTable.loots = new List<Loot>();

        Undo.RecordObject(target, "");
        GUILayout.Space(3);
        EditorGUILayout.EndVertical();
    }

    private void CurrentLootGUI(Loot loot)
    {
        if (loot == null)
            return;

        GUILayout.BeginHorizontal();

            loot.name = EditorGUILayout.TextField(loot.name);
            loot.additionalInfo = EditorGUILayout.TextField(loot.additionalInfo);
            loot.gameObject = (GameObject)EditorGUILayout.ObjectField(loot.gameObject, typeof(GameObject), true);
            loot.weight = EditorGUILayout.FloatField(loot.weight);

            Rect rect = GUILayoutUtility.GetLastRect();
            rect.x += rect.width + 4f;
            rect.width = percentWidth;
            float val = loot.percentage / 100f;

            if (!float.IsNaN(val))
            {
                EditorGUI.ProgressBar(rect, val, "" + loot.percentage);
                EditorGUILayout.LabelField("", EditorStyles.label, GUILayout.Width(percentWidth));    //Workaround Layout
            }
            else
            {
                EditorGUILayout.LabelField("NaN", EditorStyles.label, GUILayout.Width(percentWidth));
            }

            if (GUILayout.Button("-", EditorStyles.miniButton, GUILayout.Width(controlsWidth)))
                _lootTable.loots.Remove(loot);

        GUILayout.EndHorizontal();
    }

}
