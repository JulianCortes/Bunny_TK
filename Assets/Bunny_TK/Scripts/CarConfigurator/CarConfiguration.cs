using Bunny_TK.Utils;
using UnityEngine;

namespace Bunny_TK.CarConfigurator
{
    [ExecuteInEditMode]
    [System.Serializable]
    public class CarConfiguration : MonoBehaviour
    {
        public string configName;

        [SerializeField]
        protected MaterialManagerGroup _materials;

        [SerializeField]
        protected GameObjectGroup _additionalModels;

        /// <summary>
        /// If true it will autosearch MaterialManagerGroup and GameObjectGroup children.
        /// </summary>
        [SerializeField]
        protected bool autoConfig = true;

        public virtual void Apply()
        {
            if (_materials != null)
                _materials.ApplyMaterial();

            if (_additionalModels != null)
                _additionalModels.IsActive = true;
        }

        /// <summary>
        /// Disables only GameObjectGroup
        /// </summary>
        public virtual void Remove()
        {
            if (_additionalModels != null)
                _additionalModels.IsActive = false;
        }

        private void OnEnable()
        {
            if (!autoConfig) return;
            configName = name;
            _materials = GetComponentInChildren<MaterialManagerGroup>();
            _additionalModels = GetComponentInChildren<GameObjectGroup>();
        }
    }
}