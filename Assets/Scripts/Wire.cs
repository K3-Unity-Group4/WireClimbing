using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class Wire : MonoBehaviour
{
    [SerializeField] private Player _player;

    public Transform cam;
    public Transform anchor;
    private Transform hitObj;
    public RaycastHit hit;
    private Rigidbody rb;
    public bool attached = false;
    private float momentum;
    public float speed;
    private float step;
    private Vector3 raycastHitpoint;
    private Vector3 localHitPoint;
    private Vector3 worldPoint;

    [SerializeField] private TextMeshProUGUI ui;

    [SerializeField] private GameObject accelerationObject;
    private ParticleSystem acceleration;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        acceleration = accelerationObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        Ray ray = new Ray(anchor.position, anchor.forward);
        
        if(Physics.Raycast(ray, out hit, 100))
        {
            GameObject target = hit.collider.gameObject;
 
            // 右コントローラのAボタンを押した場合
            if(OVRInput.GetDown(OVRInput.RawButton.A))
            {
                ui.text = "aaaaaaaaaaa";
                target.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
                attached = true;
                rb.isKinematic = true;
                
                gameObject.transform.parent = hit.transform;
                localHitPoint = hit.transform.InverseTransformPoint(hit.point);
                hitObj = hit.transform;
                worldPoint = hit.transform.TransformPoint(localHitPoint) - hitObj.position;
                // RaycastHitpoint = hit.point;
                
                _player.enabled = false;
                accelerationObject.SetActive(true);
            }

            if (OVRInput.Get(OVRInput.RawButton.B))
            {
                if (hit.distance <= 100 && hit.distance != 0) raycastHitpoint = hitObj.position + worldPoint;
            }

            if (OVRInput.GetUp(OVRInput.RawButton.B))
            {
                attached = false;
                rb.isKinematic = false;
                var heading = raycastHitpoint - transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance;
                rb.velocity = direction * momentum;
                _player.enabled = true;
                accelerationObject.SetActive(false);
            }
        }
        
        if (OVRInput.GetDown(OVRInput.Button.Two)) ui.text = "押された";

        //Debug.Log(hit.distance);
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.distance <= 20 && hit.distance != 0)
            {
                attached = true;
                rb.isKinematic = true;
                
                gameObject.transform.parent = hit.collider.transform;
                localHitPoint = hit.transform.InverseTransformPoint(hit.point);
                hitObj = hit.transform;
                worldPoint = hit.transform.TransformPoint(localHitPoint) - hitObj.position;
                // Debug.Log(worldPoint);
                // RaycastHitpoint = hit.point;
                
                _player.enabled = false;
                accelerationObject.SetActive(true);
            }
        }

        if (Input.GetMouseButton(0))
        {
            raycastHitpoint = hitObj.position + worldPoint;
        }

        if (Input.GetMouseButtonUp(0))
        {
            attached = false;
            rb.isKinematic = false;
            gameObject.transform.parent = null;
            var heading = raycastHitpoint - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            rb.velocity = direction * momentum;
            _player.enabled = true;
            if (Vector3.Distance(raycastHitpoint, transform.position) == 0) momentum = 0;
            accelerationObject.SetActive(false);
        }
        
        if (attached)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            if (Vector3.Distance(raycastHitpoint, transform.position) <= 2)
            {
                momentum = 0;
                accelerationObject.SetActive(false);
            }
            else transform.position = Vector3.MoveTowards(transform.position, raycastHitpoint, step);
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
