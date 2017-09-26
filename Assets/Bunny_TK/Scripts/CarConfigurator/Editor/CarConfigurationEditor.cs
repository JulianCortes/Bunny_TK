using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.CarConfigurator
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CarConfiguration),true)]
    public class CarConfigurationEditor : Editor
    {


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if(GUILayout.Button("Apply Configuration"))
            {
                foreach (CarConfiguration c in targets.Cast<CarConfiguration>())
                    c.Apply();
            }

            if (GUILayout.Button("Remove Configuration"))
            {
                foreach (CarConfiguration c in targets.Cast<CarConfiguration>())
                    c.Remove();
            }
        }
    }
}
