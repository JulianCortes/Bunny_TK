using UnityEngine;

namespace Bunny_TK.DataDriven
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Data Driven/Int Variable")]
    public class IntVariable : BaseVariable<int> { }
}