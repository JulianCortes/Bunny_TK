
using System;
using UnityEngine;

//From UnityEditor.PostProcessing
[AttributeUsage(AttributeTargets.Field)]
public sealed class PropertySetAttribute : PropertyAttribute
{
    public readonly string name;
    public bool dirty;

    public PropertySetAttribute(string name)
    {
        this.name = name;
    }
}