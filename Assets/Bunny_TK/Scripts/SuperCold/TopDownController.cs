using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TopDownController : MonoBehaviour
{

    [SerializeField]
    float h = 0;
    [SerializeField]
    float v = 0;

    [SerializeField]
    float speed = 2f;

    Rigidbody _rigidbody;

    public float HorizzontalAxis { get { return h; } }
    public float VerticalAxis { get { return v; } }

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveUpate();
        LookUpdate();
    }

    void MoveUpate()
    {
        h = CrossPlatformInputManager.GetAxis("Horizontal");
        v = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0f, v);
        _rigidbody.velocity = movement * speed;
    }
    void LookUpdate()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = .0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = targetRotation;
        }
    }
}
