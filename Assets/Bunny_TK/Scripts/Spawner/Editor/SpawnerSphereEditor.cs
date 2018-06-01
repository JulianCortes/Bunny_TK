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
            SpawnerInSphere spawner = (SpawnerInSphere)target;


            if (!spawner.useMinMaxRadius)
            {
                Handles.color = Color.red;
                spawner.radius = Handles.RadiusHandle(Quaternion.identity, spawner.transform.position, spawner.radius);
            }
            else
            {
                Handles.color = Color.red;
                spawner.rangeRadius.min = Handles.RadiusHandle(Quaternion.identity, spawner.transform.position, spawner.rangeRadius.min);

                Handles.color = Color.blue;
                spawner.rangeRadius.max = Handles.RadiusHandle(Quaternion.identity, spawner.transform.position, spawner.rangeRadius.max);
            }
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
