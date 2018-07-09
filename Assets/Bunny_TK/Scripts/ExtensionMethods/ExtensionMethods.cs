using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bunny_TK
{
    public static class ExtensionMethods
    {
        public static T GetRandom<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
        public static bool AddIfNotNull<T>(this IList<T> list, T value)
        {
            if (value == null) return false;
            list.Add(value);
            return true;
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

        public static bool IsNull(this Vector3 vector)
        {
            if (vector.x <= -9999f) return true;
            if (vector.y <= -9999f) return true;
            if (vector.z <= -9999f) return true;
            return false;
        }
        public static void SetNull(this Vector3 vector)
        {
            vector.x = -9999f;
            vector.y = -9999f;
            vector.z = -9999f;
        }
        public static Vector3 NullVector3()
        {
            var vector = Vector3.zero;
            vector.x = -9999f;
            vector.y = -9999f;
            vector.z = -9999f;
            return vector;
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

        public static void CopyValues(this Transform transform, Transform original, bool position = true, bool rotation = true, bool scale = false)
        {
            if (position)
                transform.position = original.position;
            if (rotation)
                transform.rotation = original.rotation;
            if (scale)
                transform.localScale = original.localScale;
        }


    }
}
