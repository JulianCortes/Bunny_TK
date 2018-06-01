using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Spawners
{
    public class SpawnerInStack : Spawner
    {
        private bool _isActive;
        public bool isActive
        {
            get
            {
                return _isActive;
            }
        }
        public List<GameObject> prefabs;
        public Vector3 startPosition;
        public Vector3 spawnDeltaPosition;

        private Vector3 lastPosition;

        protected void Start()
        {
            base.Start();
            lastPosition = startPosition - spawnDeltaPosition;
        }

        public override GameObject GetGameObject()
        {
            return prefabs.GetRandom();
        }

        public override Vector3 GetPosition()
        {

            return transform.TransformPoint(lastPosition = lastPosition + spawnDeltaPosition);
        }
        public override int Clean()
        {
            lastPosition = startPosition - spawnDeltaPosition;
            return base.Clean();
        }
    }
}
