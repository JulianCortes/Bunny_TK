using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "LootTable", menuName = "Utilities/Loot Table")]
public class LootTableScriptableObject : ScriptableObject
{
    public string tableName;
    public List<Loot> loots;

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
}


