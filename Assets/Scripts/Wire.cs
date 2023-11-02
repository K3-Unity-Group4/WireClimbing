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
    public float speedAnchor = 5f;
    public Vector3 raycastHitpoint;
    private Vector3 localHitPoint;
    private Vector3 worldPoint;
    [SerializeField] private GameObject wire;
    private float resetTime = 0;

    // [SerializeField] private TextMeshProUGUI ui;

    [SerializeField] private GameObject accelerationObject;
    private ParticleSystem acceleration;

    [SerializeField] private AudioSource shashutu;
    [SerializeField] private AudioClip shashutuSE;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        acceleration = accelerationObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //Vector2 vectorR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        //transform.eulerAngles += new Vector3(speedAnchor * vectorR.y, speedAnchor * vectorR.x, 0);
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstick))
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }

        Ray ray = new Ray(anchor.position, anchor.forward);

        if (Physics.Raycast(ray, out hit, 20))
        {
            GameObject target = hit.collider.gameObject;

            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                shashutu.PlayOneShot(shashutuSE);

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

        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            raycastHitpoint = hitObj.position + worldPoint;
        }

        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            attached = false;
            rb.isKinematic = false;
            // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.parent = null;
            var heading = raycastHitpoint - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            if (power) rb.velocity = direction * momentum;
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
                shashutu.PlayOneShot(shashutuSE);

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
            if (power) rb.velocity = direction * momentum;
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
                if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
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
                if (Input.GetMouseButtonDown(1))
                {
                    attached = false;
                    rb.isKinematic = false;
                    power = false;
                    gameObject.transform.parent = null;
                    wire.SetActive(false);
                    _player.enabled = true;
                    accelerationObject.SetActive(false);
                    Vector3 magnitude = camVR.transform.forward * 200;
                    magnitude.y = 500;
                    rb.AddForce(magnitude);
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


        // ランキング削除
        if (OVRInput.Get(OVRInput.RawButton.LThumbstick) && OVRInput.Get(OVRInput.RawButton.LThumbstick))
        {
            resetTime += Time.deltaTime;
            if (resetTime >= 20)
            {
                UIManager.ResetData();
                resetTime = 0;
            }

            if (OVRInput.GetUp(OVRInput.RawButton.LThumbstick) || OVRInput.GetUp(OVRInput.RawButton.LThumbstick)) resetTime = 0;
        }
    }
}
