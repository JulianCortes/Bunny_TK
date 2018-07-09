using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunny_TK.DataDriven
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Text))]
    public class TextUISync : MonoBehaviour
    {
        public BaseVariable baseVariable;

        private Text textUI;

        private void OnEnable()
        {
            textUI = GetComponent<Text>();
        }
        private void Update()
        {
            if (textUI == null) return;
            if (baseVariable == null) return;
            textUI.text = baseVariable.GetStringRuntimeValue();
        }
    }
}
