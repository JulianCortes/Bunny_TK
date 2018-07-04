using UnityEngine;

namespace Bunny_TK.DataDriven
{
    /// <summary>
    /// When extending this, add "[CustomPropertyDrawer(typeof(BaseVariable<(type)>), true)]" to BaseVariableDrawer for custom drawer.
    /// </summary>
    [System.Serializable]
    public class BaseVariableGeneric<T> : BaseVariable
    {
        [SerializeField]
        public T initialValue;
        [SerializeField]
        public T runtimeValue;

        protected virtual void OnEnable()
        {
            runtimeValue = initialValue;
        }

        public override string GetStringInitialValue()
        {
            return initialValue.ToString();
        }
        public override string GetStringRuntimeValue()
        {
            return runtimeValue.ToString();
        }

        public virtual void Reset()
        {
            runtimeValue = initialValue;
        }
        public virtual void CopyTo(BaseVariableGeneric<T> variable)
        {
            variable.initialValue = initialValue;
            variable.runtimeValue = runtimeValue;
        }

        public static implicit operator T(BaseVariableGeneric<T> variable)
        {
            return variable.runtimeValue;
        }
    }

    public abstract class BaseVariable : ScriptableObject
    {
        public abstract string GetStringInitialValue();
        public abstract string GetStringRuntimeValue();
    }



}