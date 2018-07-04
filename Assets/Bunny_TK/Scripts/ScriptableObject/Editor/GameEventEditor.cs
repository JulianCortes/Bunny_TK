using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.DataDriven.CustomInspector
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Raise Event"))
            {
                (target as GameEvent).Raise();
            }
        }
    }
}
