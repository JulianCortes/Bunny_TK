using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.Spawners
{

    [CustomEditor(typeof(SpawnerInCircle))]
    public class SpawnCircleEditor : Editor
    {
        void OnSceneGUI()
        {
            Handles.color = Color.red;
            SpawnerInCircle spawner = (SpawnerInCircle)target;
            Handles.DrawWireArc(spawner.transform.position, spawner.transform.up, -spawner.transform.right, 360, spawner.radius);
            spawner.radius = Handles.ScaleValueHandle(spawner.radius,
                                                    spawner.transform.position + spawner.transform.forward * spawner.radius, spawner.transform.rotation, .5f, Handles.SphereHandleCap, 1);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Spawn"))
            {
                (target as SpawnerInCircle).Spawn();
            }

            if (GUILayout.Button("Clean"))
            {
                (target as SpawnerInCircle).Clean();
            }

        }
    }

}
