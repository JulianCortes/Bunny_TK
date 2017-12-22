using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    private float hp;

    public float HP { get { return hp; } }
    public bool IsDead { get { return hp <= 0f; } }

    public abstract bool IsDamageableBy(DamageType type);
    public abstract bool GetDamage(Damage damage);
    public abstract void Spawn();

}
