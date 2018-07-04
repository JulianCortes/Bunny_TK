using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bunny_TK.DataDriven
{
    [Serializable]
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Data Driven/Float Variable")]
    public class FloatVariable : ScriptableObject
    {
        public float initialValue;
        public float runtimeValue;

        private void OnEnable()
        {
            runtimeValue = initialValue;
        }
    }
}
