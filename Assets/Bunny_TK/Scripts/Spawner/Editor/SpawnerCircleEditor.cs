﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK.Spawners
{
    [CustomEditor(typeof(SpawnerInCircle))]
    public class SpawnCircleEditor : Editor
    {
        Vector3 maxRadius = Vector3.zero;
        Vector3 minRadius = Vector3.zero;
        Vector3 radiusCenter = Vector3.zero;

        Vector3 sliderMax = Vector3.zero;
        Vector3 sliderMin = Vector3.zero;

        void OnSceneGUI()
        {
            SpawnerInCircle spawner = (SpawnerInCircle)target;
            maxRadius.x = -Mathf.Cos(spawner.MaxAngle * Mathf.Deg2Rad);
            maxRadius.z = Mathf.Sin(spawner.MaxAngle * Mathf.Deg2Rad);

            minRadius.x = -Mathf.Cos((spawner.MinAngle) * Mathf.Deg2Rad);
            minRadius.z = Mathf.Sin((spawner.MinAngle) * Mathf.Deg2Rad);

            //if (spawner.centered)
            {
                radiusCenter = Vector3.forward;
            }
            //else
            {
                //radiusCenter.y = 0f;
                //radiusCenter.x = -Mathf.Cos(spawner.MaxAngle / 2f * Mathf.Deg2Rad);
                //radiusCenter.z = Mathf.Sin(spawner.MaxAngle / 2f * Mathf.Deg2Rad);

                radiusCenter = Vector3.forward;
            }
            Handles.color = Color.red;

            sliderMin = radiusCenter * spawner.radius.min;
            sliderMin = spawner.transform.TransformPointUnscaled(sliderMin);
            sliderMin = Handles.Slider(sliderMin, spawner.transform.TransformDirection(radiusCenter), HandleUtility.GetHandleSize(spawner.transform.position) * .2f, Handles.SphereHandleCap, 1f);
            sliderMin = spawner.transform.InverseTransformPointUnscaled(sliderMin);
            spawner.radius.min = sliderMin.z;

            sliderMax = radiusCenter * spawner.radius.max;
            sliderMax = spawner.transform.TransformPointUnscaled(sliderMax);
            sliderMax = Handles.Slider(sliderMax, spawner.transform.TransformDirection(radiusCenter), HandleUtility.GetHandleSize(spawner.transform.position) * .2f, Handles.SphereHandleCap, 1f);
            sliderMax = spawner.transform.InverseTransformPointUnscaled(sliderMax);
            spawner.radius.max = sliderMax.z;

            //Arcs
            Handles.color = Color.red;
            Handles.DrawWireArc(spawner.transform.position, spawner.transform.up, spawner.transform.TransformDirection(minRadius), spawner.angle, spawner.radius.max);
            Handles.DrawWireArc(spawner.transform.position, spawner.transform.up, spawner.transform.TransformDirection(minRadius), spawner.angle, spawner.radius.min);

            ////Max Handle
            //spawner.radius.max = Handles.ScaleValueHandle(spawner.radius.max,
            //                                          spawner.transform.TransformPointUnscaled(radiusCenter * spawner.radius.max),
            //                                          spawner.transform.rotation,
            //                                          .5f, Handles.SphereHandleCap,
            //                                          1);
            ////Min Handle
            //spawner.radius.min = Handles.ScaleValueHandle(spawner.radius.min,
            //                                          spawner.transform.TransformPointUnscaled(radiusCenter * spawner.radius.min),
            //                                          Quaternion.identity,
            //                                          .5f, Handles.SphereHandleCap,
            //                                          1);

            if (spawner.radius.min > spawner.radius.max || spawner.radius.max < spawner.radius.min)
            {
                float temp = spawner.radius.min;
                spawner.radius.min = spawner.radius.max;
                spawner.radius.max = temp;
            }

            //Angle
            Handles.color = Color.blue;
            var pos = spawner.transform.TransformPointUnscaled(maxRadius * (spawner.radius.max + spawner.radius.min) / 2f);

            float angle = Handles.ScaleValueHandle(spawner.angle,
                                      pos,
                                      spawner.transform.rotation,
                                      HandleUtility.GetHandleSize(spawner.transform.position),
                                      Handles.SphereHandleCap,
                                      1f);
            angle = Mathf.Clamp(angle, 0f, 360f);
            spawner.angle = angle;

            //Lines
            if (angle < 360)
            {
                Handles.DrawDottedLine(spawner.transform.position,
                                       spawner.transform.TransformPointUnscaled(minRadius * spawner.radius.GetMin()),
                                       1f);
                Handles.DrawDottedLine(spawner.transform.position,
                                       spawner.transform.TransformPointUnscaled(maxRadius * spawner.radius.GetMin()),
                                       1f);

                Handles.color = Color.red;
                Handles.DrawLine(spawner.transform.TransformPointUnscaled(minRadius * spawner.radius.GetMin()),
                                 spawner.transform.TransformPointUnscaled(minRadius * spawner.radius.GetMax()));

                Handles.DrawLine(spawner.transform.TransformPointUnscaled(maxRadius * spawner.radius.GetMin()),
                                 spawner.transform.TransformPointUnscaled(maxRadius * spawner.radius.GetMax()));
            }

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
