using Bunny_TK.DataDriven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protom.WallT.Istruzione.Micro
{
    [RequireComponent(typeof(Transform))]
    public class RotateOverTime : MonoBehaviour
    {
        public FloatVariable normalized;

        [SerializeField]
        protected Vector3 startRotation = Vector3.zero;
        [SerializeField]
        protected Vector3 endRotation;

        public bool loop = true;

        protected Vector3 currentRotation;

        [SerializeField]
        Transform targetTransform;

        [Header("Auto Rotation")]
        public bool autoRotate = false;
        public float step = .1f;

        protected void Update()
        {
            if (normalized == null)
                normalized = new FloatVariable();

            if (autoRotate)
                normalized.runtimeValue += step * Time.deltaTime;

            if (loop)
            {
                if (normalized.runtimeValue > 1)
                    normalized.runtimeValue -= 1f;

                if (normalized.runtimeValue < 0)
                    normalized.runtimeValue += 1f;
            }

            currentRotation = Vector3.Lerp(startRotation, endRotation, normalized);

            if (targetTransform != null)
                targetTransform.localRotation = Quaternion.Euler(currentRotation);
        }
    }
}
