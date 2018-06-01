using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Spawners
{
    public class SpawnerInSphere : Spawner
    {
        [SerializeField]
        public List<GameObject> prefabs;

        public float radius = 1f;
        public bool useMinMaxRadius = false;
        public RangeFloat rangeRadius = new RangeFloat(1f, 2f);

        public override GameObject GetGameObject()
        {
            return prefabs.GetRandom();
        }

        public override Vector3 GetPosition()
        {

            if (useMinMaxRadius)
            {
                return transform.TransformPoint(Random.onUnitSphere * rangeRadius.GetRandom());
            }

            return transform.TransformPoint(Random.insideUnitSphere * radius);
        }
    }
}
