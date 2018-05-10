using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static T GetRandom<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    private static Vector3 RandomPointInBox(this Random random, Vector3 center, Vector3 size)
    {
        return center + new Vector3(
           (Random.value - 0.5f) * size.x,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }

}
