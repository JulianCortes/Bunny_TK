using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bunny_TK.DataDriven
{
    public class BoolVariableSync : MonoBehaviour
    {
        public BoolVariable value;

        public UnityEventBool updateValue;
        private void Update()
        {
            updateValue.Invoke(value);
        }
    }
    [System.Serializable]
    public class UnityEventBool : UnityEvent<bool> { }
}
