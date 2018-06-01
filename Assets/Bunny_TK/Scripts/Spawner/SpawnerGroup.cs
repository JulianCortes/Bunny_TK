using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Spawners
{
    public class SpawnerGroup : Spawner
    {
        public List<Spawner> spawners;
        public new bool isActive
        {
            get
            {
                return isActive = _isActive;
            }
            set
            {
                base.isActive = value;
                _isActive = value;
                foreach (var spawner in spawners)
                    spawner.isActive = value;
            }
        }

        
        [SerializeField]
        private bool _isActive;

        private void Awake()
        {
            foreach (var spawner in spawners)
                spawner.enabled = false;
        }

        protected new void Start()
        {
            base.Start();
        }
        protected new void Update()
        {
            base.isActive = _isActive;
            base.Update();
        }

        public override GameObject GetGameObject()
        {
            return spawners.GetRandom().GetGameObject();
        }
        public override Vector3 GetPosition()
        {
            return spawners.GetRandom().GetPosition();
        }
    
        private void OnValidate()
        {
            foreach (var spawner in spawners)
                spawner.enabled = false;
        }
    }
}
