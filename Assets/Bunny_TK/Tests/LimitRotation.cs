using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRotation : MonoBehaviour {

    public float limit_x = 1f;
    public float limit_y = 1f;
    public float mouse_scale = 1f; // figure this one out through experimentation

    float x;
    float y;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        y += Input.GetAxis("Mouse Y") * mouse_scale;
        x += -Input.GetAxis("Mouse X") * mouse_scale;

        x = Mathf.Clamp(x, -limit_x, limit_x);
        y = Mathf.Clamp(y, -limit_y, limit_y);

        transform.rotation = Quaternion.LookRotation(-Vector3.forward + new Vector3(0, x, 0), Vector3.up + new Vector3(y, 0, 0));

    }
}
