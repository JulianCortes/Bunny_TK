using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK
{
    public static class ExtensionMethods
    {
        public static T GetRandom<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        private static Vector3 RandomPointInBox(this Random random, Vector3 center, Vector3 size)
        {
            return center + new Vector3(
               (Random.value - 0.5f) * size.x,
               (Random.value - 0.5f) * size.y,
               (Random.value - 0.5f) * size.z
            );
        }

        public static Vector3 ScreenCenter()
        {
            return new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        }


        public static float ClampAngle(float angle, float from, float to)
        {
            // accepts e.g. -80, 80
            if (angle < 0f) angle = 360 + angle;
            if (angle > 180f) return Mathf.Max(angle, 360 + from);
            return Mathf.Min(angle, to);
        }

        //https://answers.unity.com/questions/1238142/version-of-transformtransformpoint-which-is-unaffe.html
        public static Vector3 TransformPointUnscaled(this Transform transform, Vector3 position)
        {
            var localToWorldMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            return localToWorldMatrix.MultiplyPoint3x4(position);
        }

        //https://answers.unity.com/questions/1238142/version-of-transformtransformpoint-which-is-unaffe.html
        public static Vector3 InverseTransformPointUnscaled(this Transform transform, Vector3 position)
        {
            var worldToLocalMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one).inverse;
            return worldToLocalMatrix.MultiplyPoint3x4(position);
        }

        public static Vector3 GetDirection(Vector3 from, Vector3 to)
        {
            var head = to - from;
            return head / head.magnitude;
        }
    }
}
