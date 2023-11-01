using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;


public class Wire : MonoBehaviour
{
    [SerializeField] private Player _player;

    public Transform cam;
    public Transform camVR;
    public Transform anchor;
    private Transform hitObj;
    public RaycastHit hit;
    private Rigidbody rb;
    public bool attached = false;
    private bool power = true;
    private float momentum;
    public float speed;
    private float step;
    private float speedAnchor = 45f;
    public Vector3 raycastHitpoint;
    private Vector3 localHitPoint;
    private Vector3 worldPoint;
    [SerializeField] private GameObject wire;

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
        Vector2 vectorL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        camVR.transform.eulerAngles += new Vector3(speedAnchor * vectorL.y, speedAnchor * vectorL.x, 0);
        
        Ray ray = new Ray(anchor.position, anchor.forward);
        
        if(Physics.Raycast(ray, out hit, 100))
        {
            GameObject target = hit.collider.gameObject;

            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
                attached = true;
                rb.isKinematic = true;
                
                gameObject.transform.parent = hit.collider.transform;
                localHitPoint = hit.transform.InverseTransformPoint(hit.point);
                hitObj = hit.transform;
                worldPoint = hit.transform.TransformPoint(localHitPoint) - hitObj.position;
                // RaycastHitpoint = hit.point;
                
                wire.SetActive(true);
                _player.enabled = false;
                accelerationObject.SetActive(true);
            }
        }
        
        if (OVRInput.Get(OVRInput.RawButton.B))
        {
            raycastHitpoint = hitObj.position + worldPoint;
        }

        if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            attached = false;
            rb.isKinematic = false;
            // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.parent = null;
            var heading = raycastHitpoint - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            if(power) rb.velocity = direction * momentum;
            power = true;
            wire.SetActive(false);
            _player.enabled = true;
            if (Vector3.Distance(raycastHitpoint, transform.position) == 0) momentum = 0;
            accelerationObject.SetActive(false);
        }
        
        // if (OVRInput.GetDown(OVRInput.Button.Two)) ui.text = "押された";

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
                
                wire.SetActive(true);
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
            // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.parent = null;
            var heading = raycastHitpoint - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            if(power) rb.velocity = direction * momentum;
            power = true;
            wire.SetActive(false);
            _player.enabled = true;
            if (Vector3.Distance(raycastHitpoint, transform.position) == 0) momentum = 0;
            accelerationObject.SetActive(false);
        }
        
        if (attached)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            if (Vector3.Distance(raycastHitpoint, transform.position) <= 1.5f)
            {
                momentum = 0;
                accelerationObject.SetActive(false);
                
                // 右コントローラのAボタンを押した場合
                if(OVRInput.GetDown(OVRInput.RawButton.A))
                {
                    attached = false;
                    rb.isKinematic = false;
                    power = false;
                    gameObject.transform.parent = null;
                    wire.SetActive(false);
                    _player.enabled = true;
                    accelerationObject.SetActive(false);
                    rb.AddForce(0, 500f, 0);
                }
                
                // 右コントローラのAボタンを押した場合
                if(Input.GetMouseButtonDown(1))
                {
                    attached = false;
                    rb.isKinematic = false;
                    power = false;
                    gameObject.transform.parent = null;
                    wire.SetActive(false);
                    _player.enabled = true;
                    accelerationObject.SetActive(false);
                    
                    rb.AddForce(0, 500f, 0);
                }
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
