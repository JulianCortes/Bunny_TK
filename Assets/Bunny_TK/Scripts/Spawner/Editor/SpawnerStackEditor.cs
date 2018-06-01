using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.Spawners
{

    [CustomEditor(typeof(SpawnerInStack))]
    public class SpawnerStackEditor : Editor
    {

        private SpawnerInStack spawner { get { return target as SpawnerInStack; } }
        private static Vector3 labelOffset = new Vector3(.1f, .1f, .1f);
        private int selectedIndex = -1;
        public const float handleSize = .3f;
        public const float pickSize = .3f;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Spawn"))
            {
                spawner.Spawn();
            }

            if (GUILayout.Button("Clean"))
            {
                spawner.Clean();
            }
        }

        private void OnSceneGUI()
        {
            Vector3 startPoint = spawner.transform.TransformPoint(spawner.startPosition);
            float size = HandleUtility.GetHandleSize(startPoint);

            Handles.Label(startPoint + labelOffset, "Start" + spawner.startPosition);

            //if (Handles.Button(startPoint,
            //                   Quaternion.identity,
            //                   size * handleSize,
            //                   size * pickSize, Handles.SphereHandleCap))
            {
                Repaint();
                EditorGUI.BeginChangeCheck();
                startPoint = Handles.DoPositionHandle(startPoint, Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Move Point");
                    EditorUtility.SetDirty(target);
                    spawner.startPosition = spawner.transform.InverseTransformPoint(startPoint);
                }
            }

            Vector3 deltaPos = spawner.transform.TransformPoint(spawner.spawnDeltaPosition+spawner.startPosition);
            Handles.Label(deltaPos + labelOffset, "Delta" + spawner.spawnDeltaPosition);

            //if (Handles.Button(deltaPos,
            //                   Quaternion.identity,
            //                   size * handleSize,
            //                   size * pickSize, Handles.SphereHandleCap))
            {
                Repaint();
                EditorGUI.BeginChangeCheck();
                deltaPos = Handles.DoPositionHandle(deltaPos, Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Move Point");
                    EditorUtility.SetDirty(target);
                    spawner.spawnDeltaPosition = spawner.transform.InverseTransformPoint(deltaPos-spawner.startPosition);
                }
            }

            Handles.DrawLine(spawner.transform.TransformPoint(spawner.startPosition), spawner.transform.TransformPoint(spawner.spawnDeltaPosition+spawner.startPosition));

        }
    }
}
