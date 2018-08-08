using Bunny_TK.DataDriven.UI.Template;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenericTemplate<>), true)]
public class BaseTemplateInspector : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //EditorGUILayout.LabelField(ObjectNames.NicifyVariableName(target.GetType().Name));

        BaseTemplate baseTemplate = target as BaseTemplate;
        string labelButton = baseTemplate.continuosRefreshAll ? "Disable" : "Enable";

        if (GUILayout.Button(labelButton + " Continuos Refresh All"))
        {
            baseTemplate.ToggleContinuosRefreshAll();
        }

        EditorGUI.BeginDisabledGroup(baseTemplate.continuosRefreshAll);

        if (GUILayout.Button("Manual Refresh All"))
        {
            baseTemplate.UpdateAll();
            SceneView.RepaintAll();
        }

        EditorGUI.EndDisabledGroup();

        if(GUILayout.Button("Find References In Scene"))
        {
            Selection.objects = baseTemplate.FindReferencesInScene().ToArray();
        }
    }
}
