using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Spawners
{
    public class SpawnerInCircle : Spawner
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
            Vector2 targetPos = Random.insideUnitCircle * radius;
            return  transform.TransformPoint( new Vector3(targetPos.x, 0f, targetPos.y));
        }

    }
}
