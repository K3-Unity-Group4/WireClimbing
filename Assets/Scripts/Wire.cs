using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wire : MonoBehaviour
{
    [SerializeField] private Player _player;

    public Transform cam;
    public RaycastHit hit;
    private Rigidbody rb;
    public bool attached = false;
    private float momentum;
    public float speed;
    private float step;
    private Vector3 RaycastHitpoint;

    [SerializeField] private GameObject accelerationObject;
    private ParticleSystem acceleration;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        acceleration = accelerationObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //Debug.Log(hit.distance);
        if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (hit.distance <= 20 && hit.distance != 0)
            {
                attached = true;
                rb.isKinematic = true;
                RaycastHitpoint = hit.point;
                _player.enabled = false;
                accelerationObject.SetActive(true);
            }
        }
        
        if (Input.GetMouseButtonUp(0) || OVRInput.GetUp(OVRInput.Button.Two))
        {
            attached = false;
            rb.isKinematic = false;
            var heading = RaycastHitpoint - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            rb.velocity = direction * momentum;
            _player.enabled = true;
            if (Vector3.Distance(RaycastHitpoint, transform.position) == 0) momentum = 0;
            accelerationObject.SetActive(false);
        }
        
        if (attached)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, RaycastHitpoint, step);
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
