using UnityEngine;

namespace Bunny_TK.DataDriven
{
    /// <summary>
    /// When extending this, add "[CustomPropertyDrawer(typeof(BaseVariable<(type)>), true)]" to BaseVariableDrawer for custom drawer.
    /// </summary>
    [System.Serializable]
    public abstract class BaseVariable<T> : ScriptableObject
    {
        [SerializeField]
        public T initialValue;
        [SerializeField]
        public T runtimeValue;

        protected virtual void OnEnable()
        {
            runtimeValue = initialValue;
        }
    }
}