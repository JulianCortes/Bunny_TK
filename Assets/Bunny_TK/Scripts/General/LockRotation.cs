using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LockRotation : MonoBehaviour
{
    public Vector3 targetRotation;
    public bool lockRot = false;


    // Update is called once per frame
    void Update()
    {
        if (lockRot)
            transform.rotation = Quaternion.Euler(targetRotation);
    }
}
