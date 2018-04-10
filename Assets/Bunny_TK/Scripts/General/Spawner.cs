using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Utils
{
    public class Spawner : MonoBehaviour
    {
        public List<Transform> positions;
        public List<GameObject> prefabs;
        public Transform parentTransform;

        public event Action<GameObject> Spawned;
        public UnityEventGameObject OnSpawned;

        private void Start()
        {
            if (parentTransform == null)
            {
                var p = new GameObject();
                p.transform.position = this.transform.position;
                p.name = "_Spawned_" + this.name;
                parentTransform = p.transform;
            }

            if (positions == null)
                positions = new List<Transform>();

            if (positions.Count == 0)
                positions.Add(this.transform);
        }

        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject spawned = Instantiate(prefab, position, rotation);
            spawned.transform.SetParent(parentTransform, true);

            _OnSpawned(spawned);

            return spawned;
        }

        public GameObject Spawn(int prefabIndex = 0, int positionIndex = 0)
        {
            return Spawn(prefabs[prefabIndex], positions[0].position, positions[0].rotation);
        }

        public GameObject SpawnRandomPrefabAtPos(int positionIndex = 0)
        {
            return Spawn(RandomIndexPrefab(), positionIndex);
        }

        public GameObject SpawnPrefabAtRandomPos(int prefabIndex = 0)
        {
            return Spawn(prefabIndex, RandomIndexPositions());
        }

        [MethodButton]
        public GameObject SpawnRandomPrefabAtRandomPos()
        {
            return Spawn(RandomIndexPrefab(), RandomIndexPositions());
        }

        /// <summary>
        /// This destroys items child of parentTransform
        /// </summary>
        [MethodButton]
        public void ClearSpawned()
        {
            List<Transform> temp = new List<Transform>();
            foreach (Transform child in parentTransform)
                temp.Add(child);
            foreach (Transform child in temp)
                Destroy(child.gameObject);
        }

        public static int GetRandomWeightedIndex(float[] weights)
        {
            if (weights == null || weights.Length == 0) return -1;

            float currentWeight;
            float totalWeight = 0f;
            int index;

            for (index = 0; index < weights.Length; index++)
            {
                currentWeight = weights[index];
                if (float.IsPositiveInfinity(currentWeight))
                    return index;
                else if (currentWeight >= 0f && !float.IsNaN(currentWeight))
                    totalWeight += weights[index];
            }

            float randomValue = UnityEngine.Random.value;
            float cumulative = 0f;

            for (index = 0; index < weights.Length; index++)
            {
                currentWeight = weights[index];
                if (float.IsNaN(currentWeight) || currentWeight <= 0f)
                    continue;

                cumulative += currentWeight / totalWeight;
                if (cumulative >= randomValue)
                    return index;
            }

            return -1;
        }

        protected virtual int RandomIndexPrefab()
        {
            return UnityEngine.Random.Range(0, prefabs.Count);
        }

        protected virtual int RandomIndexPositions()
        {
            return UnityEngine.Random.Range(0, positions.Count);
        }

        private void _OnSpawned(GameObject gameObject)
        {
            OnSpawned.Invoke(gameObject);

            if (Spawned != null)
                Spawned(gameObject);
        }
    }
}