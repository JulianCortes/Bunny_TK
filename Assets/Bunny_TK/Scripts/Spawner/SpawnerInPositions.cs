using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Spawners
{
    public class SpawnerInPositions : Spawner
    {
        [SerializeField]
        List<GameObject> prefabs;
        public List<Vector3> positions;

        protected override GameObject GetGameObject()
        {
            return prefabs.GetRandom();
        }

        protected override Vector3 GetPosition()
        {
            return transform.TransformPoint(positions.GetRandom());
        }
    }
}
