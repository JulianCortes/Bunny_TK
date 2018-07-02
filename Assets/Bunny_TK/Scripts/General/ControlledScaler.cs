using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ControlledScaler : MonoBehaviour
{
    public Transform target;
    public Vector3 scaler;
    public float speed = 1f;

    [Range(0f, 1f)]
    [SerializeField]
    private float normalized = 0f;

    [SerializeField]
    private Vector3 startScale;

    private float targetNormal;
    public float Normalized
    {
        get
        {
            return normalized;
        }
        set
        {
            if (float.IsNaN(value))
                targetNormal = 0f;
            else
                targetNormal = Mathf.Clamp01(value);
        }
    }

    private void Update()
    {
        if (Application.isPlaying)
            normalized = Mathf.Lerp(normalized, targetNormal, Time.deltaTime * speed);
        target.localScale = startScale + (scaler * normalized);
    }
}
