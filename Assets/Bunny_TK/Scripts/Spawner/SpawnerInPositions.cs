using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Spawners
{
    public class SpawnerInPositions : Spawner
    {
        [SerializeField]
        public List<GameObject> prefabs;
        public List<Vector3> positions;

        public override GameObject GetGameObject()
        {
            return prefabs.GetRandom();
        }

        public override Vector3 GetPosition()
        {
            return transform.TransformPoint(positions.GetRandom());
        }
    }
}
