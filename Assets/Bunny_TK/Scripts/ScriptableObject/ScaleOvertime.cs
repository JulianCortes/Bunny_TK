using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.DataDriven
{
    [ExecuteInEditMode]
    public class ScaleOvertime : MonoBehaviour
    {
        public FloatVariable normalized;
        public AnimationCurve curveX;
        public AnimationCurve curveY;
        public AnimationCurve curveZ;

        public bool localScaling = true;
        public Vector3Variable value;

        private void Update()
        {
            if (normalized == null)
                normalized = new FloatVariable();

            if (value == null)
                value = new Vector3Variable();

            value.runtimeValue.x = curveX.Evaluate(normalized);
            value.runtimeValue.y = curveY.Evaluate(normalized);
            value.runtimeValue.z = curveZ.Evaluate(normalized);

            transform.localScale = value.runtimeValue;
        }

    }
}
