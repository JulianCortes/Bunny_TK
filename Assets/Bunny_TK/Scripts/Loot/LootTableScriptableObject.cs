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
        // - Add multipliers
        // - Add wrappers for common collection functionalities
        //      - Make loots private
        //      - Add Add/Remove
        //      - Add Count
        // -

        public string tableName;
        public List<Loot> loots;

        public Loot this[int index]
        {
            get
            {
                return loots[index];
            }

            set
            {
                loots[index] = value;
            }
        }
        public int Count
        {
            get
            {
                return loots.Count;
            }
        }

        private float totalWeight;

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
            int index = GetRandomWeightedIndex(loots.Select(l => l.weight).ToArray());
            if (index < 0) return null;
            return loots[index];
        }

        public void UpdatePercentages()
        {
            UpdatePercentages(loots);
        }

        private void UpdatePercentages(List<Loot> loots)
        {
            float totalWeight = 0f;
            loots.ForEach(l => totalWeight += l.weight);
            loots.ForEach(l => l.percentage = (l.weight / totalWeight) * 100f);
        }

        //List funcs wrapper

        public void Add(Loot item)
        {
            loots.Add(item);
            loots.ForEach(l => totalWeight += l.weight);
        }

        public bool Remove(Loot item)
        {
            bool res = loots.Remove(item);
            loots.ForEach(l => totalWeight += l.weight);
            return res;
        }

        public float GetPercentage(Loot item)
        {
            if (item == null) return 0f;
            if (loots.Count == 0 && item.weight > 0) return 1f;

            float val = 0f;

            if (loots.Contains(item))
            {
                val = item.weight / totalWeight;
            }
            else
            {
                float tempWeight = item.weight;
                loots.ForEach(l => tempWeight += l.weight);

                val = item.weight / totalWeight;
            }

            if (float.IsNaN(val))
                val = 0f;

            return val;
        }


    }


}