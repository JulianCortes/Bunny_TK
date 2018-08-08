using System;
using UnityEngine;

namespace Bunny_TK.DataDriven
{
    [Serializable]
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "Data Driven/Bool Variable")]
    public class BoolVariable : BaseVariableGeneric<bool>
    {
        public bool RuntimeValue
        {
            get
            {
                return runtimeValue;
            }

            set
            {
                runtimeValue = value;
            }
        }
    }
}