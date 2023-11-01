using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Transform cam;
    
    void Start()
    {
        
    }
    
    
    void Update()
    {
        Vector3 moveVector = GetMoveVector();
        bool isMove = moveVector != Vector3.zero;

        if (isMove)
        {
            Move(moveVector);
        }
        
        
        
        
    }
    
    private Vector3 GetMoveVector()
    {
        Vector3 moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            moveVector += cam.transform.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            moveVector += -cam.transform.forward;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveVector += -cam.transform.right;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveVector += cam.transform.right;
        }
        
        Vector2 vectorR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        moveVector += vectorR.x * cam.transform.right;
        moveVector += vectorR.y * cam.transform.forward;
        
        return moveVector.normalized;
    }

    void Move(Vector3 moveVector)
    {
        // 移動処理
        Vector3 moveDelta;
        moveDelta = moveVector * Time.deltaTime * speed;
        transform.position += moveDelta;
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Block"))
        {
            gameObject.transform.parent = col.gameObject.transform;
        }
    }
    
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Block"))
        {
            gameObject.transform.parent = null;
        }
    }
}
