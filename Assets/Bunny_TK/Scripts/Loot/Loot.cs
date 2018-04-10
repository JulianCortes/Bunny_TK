using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class Loot
{ 
    public string name;
    public string additionalInfo;
    public GameObject gameObject;

    public float weight;
    public float percentage;

    public override string ToString()
    {
        return name + " " + weight; 
    }
}


