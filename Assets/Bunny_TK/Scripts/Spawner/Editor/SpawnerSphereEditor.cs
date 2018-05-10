using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.Spawners
{

    [CustomEditor(typeof(SpawnerInSphere))]
    public class SpawnSphereEditor : Editor
    {
        void OnSceneGUI()
        {
            Handles.color = Color.red;
            SpawnerInSphere spawner = (SpawnerInSphere)target;
            spawner.radius = Handles.RadiusHandle(Quaternion.identity, spawner.transform.position, spawner.radius);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Spawn"))
            {
                (target as Spawner).Spawn();
            }

            if (GUILayout.Button("Clean"))
            {
                (target as Spawner).Clean();
            }

        }
    }

}
