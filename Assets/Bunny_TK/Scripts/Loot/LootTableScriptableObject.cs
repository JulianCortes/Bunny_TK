using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunny_TK.Loot
{
    [Serializable]
    [CreateAssetMenu(fileName = "LootTable", menuName = "Utilities/Loot Table")]
    public class LootTableScriptableObject : ScriptableObject
    {
        //TODO:
        // - multipliers

        public string tableName;

        public float TotalWeight
        {
            get
            {
                float w = 0f;
                _loots.ForEach(l => w += l.weight);
                return w;
            }
        }

        private List<Loot> _loots;

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

                //Check if in range
                cumulative += currentWeight / totalWeight;
                if (cumulative >= randomValue)
                    return index;
            }

            return -1;
        }

        public Loot GetRandomWeighted()
        {
            int index = GetRandomWeightedIndex(_loots.Select(l => l.weight).ToArray());
            if (index < 0) return null;
            return _loots[index];
        }

        public float GetPercentage(Loot item)
        {
            if (item == null) return 0f;
            if (_loots.Count == 0 && item.weight > 0) return 1f;

            float val = 0f;

            if (_loots.Contains(item))
            {
                val = item.weight / TotalWeight;
            }
            else
            {
                float tempWeight = item.weight;
                _loots.ForEach(l => tempWeight += l.weight);

                val = item.weight / TotalWeight;
            }

            if (float.IsNaN(val))
                val = 0f;

            return val;
        }

        //List funcs wrapper
        public Loot this[int index]
        {
            get
            {
                return _loots[index];
            }

            set
            {
                _loots[index] = value;
            }
        }

        public int Count
        {
            get
            {
                if (_loots == null) return 0;
                return _loots.Count;
            }
        }

        public void Add(Loot item)
        {
            _loots.Add(item);
        }

        public bool Remove(Loot item)
        {
            bool res = _loots.Remove(item);
            return res;
        }

        public bool Contains(Loot item)
        {
            return _loots.Contains(item);
        }

        public void Clear()
        {
            _loots = new List<Loot>();
        }

        public List<Loot> GetLoots()
        {
            return this;
        }

        public static implicit operator List<Loot>(LootTableScriptableObject lootTable)
        {
            return new List<Loot>(lootTable._loots);
        }
    }
}