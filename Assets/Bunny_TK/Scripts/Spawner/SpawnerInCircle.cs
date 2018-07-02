using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Spawners
{

    public class SpawnerInCircle : Spawner
    {
        //Scale is ignored
        [SerializeField]
        public List<GameObject> prefabs;
        [Range(0f, 360f)]
        public float angle = 360f;
        public RangeFloat radius = new RangeFloat(0f, 1f);
        public const bool centered = true;
        

        public float MinAngle
        {
            get
            {
                if (centered)
                    return 90f - (angle / 2f);
                else
                    return 0f;
            }
        }
        public float MaxAngle
        {
            get
            {
                if (centered)
                    return 90f + (angle / 2f);
                else
                    return angle;
            }
        }

        public override GameObject GetGameObject()
        {
            return prefabs.GetRandom();
        }

        public override Vector3 GetPosition()
        {
            Vector2 targetPos = GetPosition(angle);
            return transform.TransformPointUnscaled(new Vector3(-targetPos.x, 0f, targetPos.y));
        }

        protected Vector2 GetPosition(float angle)
        {

            float a = Random.Range(MinAngle * Mathf.Deg2Rad, MaxAngle * Mathf.Deg2Rad);
            Vector2 pos = Vector3.zero;

            pos.x = Mathf.Cos(a);
            pos.y = Mathf.Sin(a);

            return pos * radius.GetRandom();
        }

        private void OnValidate()
        {
            angle = Mathf.Clamp(angle, 0f, 360f);
        }

    }
}
