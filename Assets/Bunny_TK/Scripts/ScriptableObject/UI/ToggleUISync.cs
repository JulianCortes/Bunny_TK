using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunny_TK.DataDriven.UI
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleUISync : MonoBehaviour
    {
        public BoolVariable variable;
        public Toggle toggle;

        private void OnValidate()
        {
            toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void Update()
        {
            if (variable == null)
                variable = new BoolVariable();
            toggle.isOn = variable.runtimeValue;
        }

        private void OnValueChanged(bool value)
        {
            variable.runtimeValue = value;
        }

    }
}
