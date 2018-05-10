using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Spawners
{
    public class SpawnerInSphere : Spawner
    {
        [SerializeField]
        protected List<GameObject> prefabs;

        public float radius = 1f;

        protected override GameObject GetGameObject()
        {
            return prefabs.GetRandom();
        }

        protected override Vector3 GetPosition()
        {
            return transform.TransformPoint(Random.insideUnitSphere * radius);
        }
    }
}
