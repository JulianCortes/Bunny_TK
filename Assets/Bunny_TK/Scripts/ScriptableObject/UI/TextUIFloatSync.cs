using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunny_TK.DataDriven.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Text))]
    public class TextUIFloatSync : MonoBehaviour
    {
        public FloatVariable floatVariable;
        public string format = "F2";

        private Text text;
        private void OnEnable()
        {
            text = GetComponent<Text>();
        }

        private void Update()
        {
            if (floatVariable != null)
                text.text = floatVariable.runtimeValue.ToString(format);
        }

    }
}
