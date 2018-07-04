using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bunny_TK.DataDriven;
public class SampleDrawers : MonoBehaviour {

    public IntVariable intVariable;
    public FloatVariable floatVariable;
    public StringVariable stringVariable;
    public Vector3Variable vec3Variable;

    [RangeFloatMinMax(-10f, 10f)]
    public RangeFloat rangeFloatAttributed;

    public RangeFloat rangeFloat;

}
