using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangeFloat
{
    public float min;
    public float max;

    public RangeFloat(float min, float max)
    {
        this.min = min;
        this.max = max;
        if (this.min > this.max)
        {
            float temp = this.min;
            this.min = this.max;
            this.max = temp;
        }
    }

    public bool CheckInRange(float value)
    {
        return value >= min && value <= max;
    }
    public float InverseLerp(float value)
    {
        return Mathf.InverseLerp(min, max, value);
    }
    public float Lerp(float value)
    {
        return Mathf.Lerp(min, max, value);
    }

    public float GetRandom()
    {
        return UnityEngine.Random.Range(min, max);
    }
    public float GetMax()
    {
        return max >= min ? max : min;
    }
    public float GetMin()
    {
        return min <= max ? min : max;
    }
    public float GetDelta()
    {
        return Mathf.Abs(min - max);
    }

    public void AddOffset(float offset)
    {
        min += offset;
        max += offset;
    }
}