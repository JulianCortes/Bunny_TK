using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.DataDriven.UI.Template
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BaseStyle), true)]
    public class BaseStyleEditor : Editor
    {
        private static string lastPath = "";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BaseStyle baseStyle = target as BaseStyle;

            if (GUILayout.Button("Refresh"))
            {
                baseStyle.ApplyTemplate();
                EditorUtility.SetDirty(target);
            }

            if (GUILayout.Button("Update Template"))
            {
                if (baseStyle.BaseTemplate != null)
                {
                    Undo.RecordObject(baseStyle.BaseTemplate, $"Modified {baseStyle.BaseTemplate.name}");
                    baseStyle.UpdateTemplate();
                    EditorUtility.SetDirty(target);
                }
            }

            if (GUILayout.Button("Save To New"))
            {
                var newTemplate = baseStyle.CreateNewTemplate();
                if (baseStyle.BaseTemplate == null)
                {
                    baseStyle.CopySettingsToTemplate(newTemplate);
                }
                else
                {
                    //Create a copy of current
                    newTemplate.CopyFrom(baseStyle.BaseTemplate);
                    newTemplate.name += "Copy";
                }
                SaveToFile(newTemplate);
            }
        }

        private void SaveToFile(BaseTemplate baseTemplate)
        {
            if (string.IsNullOrEmpty(lastPath))
                lastPath = Application.dataPath;

            lastPath = EditorUtility.SaveFilePanel("Save Template", lastPath, baseTemplate.name, "asset");
            if (lastPath.Length != 0)
            {
                string relativePath = "";
                if (lastPath.StartsWith(Application.dataPath))
                    relativePath = "Assets" + lastPath.Substring(Application.dataPath.Length);

                AssetDatabase.CreateAsset(baseTemplate, relativePath);
                AssetDatabase.Refresh();

                (target as BaseStyle).BaseTemplate = baseTemplate;
                EditorUtility.SetDirty(target);
            }
        }
    }
}
