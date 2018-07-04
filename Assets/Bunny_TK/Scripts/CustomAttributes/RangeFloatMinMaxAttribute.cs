using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attribute for showing MinMax Slider for RangeFloat.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class RangeFloatMinMaxAttribute : PropertyAttribute
{
    public float minLimit;
    public float maxLimit;

    public RangeFloatMinMaxAttribute(float minLimit, float maxLimit)
    {
        this.minLimit = minLimit;
        this.maxLimit = maxLimit;
    }
}
