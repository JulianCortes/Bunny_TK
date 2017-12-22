using Bunny_TK.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyControlled : MonoBehaviour
{
    Rigidbody _rigidbody;

    [SerializeField]
    Vector3 currentVelocity;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        currentVelocity = _rigidbody.velocity = _rigidbody.velocity * TimeManager.Instance.scaler;
    }

}
