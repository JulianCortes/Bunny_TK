using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.Spawners
{
    public abstract class Spawner : MonoBehaviour
    {
        [Header("Settings")]
        public bool isActive;               //Currently "auto spawning"
        public float frequency;             //
        public int maxSpawnCount;           //How many can exist concurrently
        public int targetTotalSpawnCount;   //How many item to spawn in total
        public bool spawnAtStart;           //spawn one at start

        [Header("References")]
        [SerializeField]
        protected Transform spawnParent;
        [SerializeField]
        protected float timeSinceLast;
        [SerializeField]
        protected List<GameObject> spawns;
        [SerializeField]
        protected int totalSpawned;

        public event Action<GameObject> OnSpawn;

        public GameObject LastSpawn { get { return spawns[spawns.Count - 1]; } }
        public float TimeSinceLast { get { return timeSinceLast; } }
        public float TotalSpawned { get { return totalSpawned; } }
        public int CurrentSpawnCount { get { return spawns.Count; } }
        public List<GameObject> Spawns { get { return new List<GameObject>(spawns); } }

        protected void Start()
        {
            if (spawnAtStart)
                Spawn();
        }
        protected void Update()
        {
            if (!isActive) return;
            if (frequency < 0) return;
            if (maxSpawnCount >= 1 && spawns.Count >= maxSpawnCount) return;
            if (targetTotalSpawnCount >= 1 && totalSpawned >= targetTotalSpawnCount) return;

            timeSinceLast += Time.deltaTime;
            if (timeSinceLast >= frequency)
                Spawn();
        }

        public abstract Vector3 GetPosition();
        public abstract GameObject GetGameObject();

        public virtual int Clean()
        {
            totalSpawned = 0;
            int count = spawns.Count;
            foreach (var spawn in spawns)
                if (spawn != null)
                    Destroy(spawn);

            spawns.Clear();
            return count;
        }
        public virtual bool DestroySpawn(GameObject target)
        {
            if (!RemoveSpawn(target)) return false;
            Destroy(target);
            return true;
        }
        public virtual bool RemoveSpawn(GameObject target)
        {
            if (target == null) return false;
            if (!spawns.Contains(target)) return false;

            spawns.Remove(target);
            return true;
        }
        public virtual void SetActiveSpawns(bool isActive)
        {
            foreach (var spawn in spawns)
                if (spawn != null)
                    spawn.SetActive(isActive);

        }

        public GameObject Spawn()
        {
            return Spawn(GetGameObject());
        }
        public GameObject Spawn(GameObject gameObject)
        {
            return Spawn(gameObject, Quaternion.identity);
        }
        public GameObject Spawn(GameObject gameObject, Quaternion rotation)
        {
            return Spawn(gameObject, GetPosition(), rotation);
        }
        protected virtual GameObject Spawn(GameObject gameObject, Vector3 position, Quaternion rotation)
        {
            if (gameObject == null) return null;
            GameObject spawn = Instantiate(gameObject);
            spawn.transform.position = position;
            spawn.transform.rotation = rotation;
            spawn.transform.SetParent(spawnParent);

            timeSinceLast = 0f;
            spawns.Add(spawn);
            totalSpawned++;
            if (OnSpawn != null)
                OnSpawn(spawn);
            return spawn;
        }
    }
}
