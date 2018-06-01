using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.Spawners
{
    [CustomEditor(typeof(SpawnerInPositions))]
    public class SpawnerPositionsEditor : Editor
    {
        private int selectedIndex = -1;
        private const float handleSize = 0.32f;
        private const float pickSize = .5f;
        private Vector3 labelOffest = new Vector3(.1f, .1f, .1f);

        SpawnerInPositions spawner { get { return target as SpawnerInPositions; } }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            EditorGUILayout.Space();


            if (GUILayout.Button("Add Position"))
            {
                if (spawner.positions == null)
                    spawner.positions = new List<Vector3>();
                spawner.positions.Add(spawner.transform.InverseTransformPoint(spawner.transform.position) + Vector3.up);
            }

            if(GUILayout.Button("Spawn"))
            {
                spawner.Spawn();
            }

            if(GUILayout.Button("Clean"))
            {
                spawner.Clean();
            }
        }
        private void OnSceneGUI()
        {
            if (spawner.positions == null)
                spawner.positions = new List<Vector3>();
            for (int i = 0; i < spawner.positions.Count; i++)
                ShowPoint(i);
        }

        private Vector3 ShowPoint(int index)
        {

            Vector3 point = spawner.transform.TransformPoint((target as SpawnerInPositions).positions[index]);
            float size = HandleUtility.GetHandleSize(point);

            Handles.Label(point + labelOffest, "" + index + " " + spawner.positions[index]);

            if (Handles.Button(point,
                               Quaternion.identity,
                               size * handleSize,
                               size * pickSize, Handles.SphereHandleCap))
            {
                selectedIndex = index;
                Repaint();
            }
            if (selectedIndex == index)
            {
                EditorGUI.BeginChangeCheck();
                point = Handles.DoPositionHandle(point, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Move Point");
                    EditorUtility.SetDirty(target);
                    (target as SpawnerInPositions).positions[index] = spawner.transform.InverseTransformPoint(point);
                }
            }
            return point;
        }
    }

}
