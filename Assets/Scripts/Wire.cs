using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wire : MonoBehaviour
{
    public Transform cam;
    public RaycastHit hit;
    private Rigidbody rb;
    public bool attached = false;
    private float momentum;
    public float speed;
    private float step;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Debug.Log(hit.distance);
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.distance <= 20 && hit.distance != 0)
            {
                attached = true;
                rb.isKinematic = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            attached = false;
            rb.isKinematic = false;
            rb.velocity = cam.forward * momentum;
        }
        if (attached)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
        }
        if (!attached && momentum >= 0)
        {
            momentum -= Time.deltaTime * 5;
            step = 0;
        }
        if (momentum <= 0)
        {
            momentum = 0;
            step = 0;
        }
    }
}
