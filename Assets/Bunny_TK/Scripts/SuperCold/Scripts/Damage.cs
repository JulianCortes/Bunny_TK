using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float amount;
    public DamageType type;
}

public enum DamageType
{
    None,
    FireArm,
    Melee
}
