using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カメラの制御
public class Axis : MonoBehaviour
{   
    float angleUp = 60f;
    float angleDown = -60f;
    [SerializeField] GameObject player;
    [SerializeField] Camera cam;
    [SerializeField] float rotate_speed = 3;
    [SerializeField] Vector3 axisPos;
    [SerializeField] float scroll;
    [SerializeField] float scrollLog;
    // Start is called before the first frame update
    void Start()
    {
        cam.transform.localPosition = new Vector3(0, 0.7f, 0);
        cam.transform.localRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + axisPos;
        scroll = Input.GetAxis("Mouse ScrollWheel");
        scrollLog += Input.GetAxis("Mouse ScrollWheel");
        cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z + scroll);
        
        transform.eulerAngles += new Vector3(Input.GetAxis("Mouse Y") * -rotate_speed, Input.GetAxis("Mouse X") * rotate_speed, 0);
        
        float angleX = transform.eulerAngles.x;
        
        if (angleX >= 180)
        {
            angleX = angleX - 360;
        }
        
        transform.eulerAngles = new Vector3(Mathf.Clamp(angleX, angleDown, angleUp), transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
