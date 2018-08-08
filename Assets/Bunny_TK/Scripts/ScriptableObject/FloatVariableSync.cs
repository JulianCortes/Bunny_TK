using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bunny_TK.DataDriven
{
    public class FloatVariableSync : MonoBehaviour
    {
        public FloatVariable value;

        public UnityEventFloat updateValue;

        private void Update()
        {
            updateValue.Invoke(value);
        }

    }
    [System.Serializable]
    public class UnityEventFloat : UnityEvent<float> { }
}
