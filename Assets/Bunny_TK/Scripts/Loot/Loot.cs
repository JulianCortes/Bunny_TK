using System.Runtime.Serialization;
using UnityEngine;
namespace Bunny_TK.Loot
{
    [System.Serializable]
    public class Loot
    {
        public string name;
        public string additionalInfo;
        public GameObject gameObject;

        public float weight;
        public float percentage; //Remove this

        public override string ToString()
        {
            return name + " " + weight;
        }

        public int GetInt()
        {
            int val = 0;
            int.TryParse(additionalInfo, out val);
            return val;
        }

        public float GetFloat()
        {
            float val = 0f;
            float.TryParse(additionalInfo, out val);
            return val;
        }
    }
}


