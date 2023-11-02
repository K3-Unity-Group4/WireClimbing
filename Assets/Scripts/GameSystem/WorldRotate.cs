using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotate : MonoBehaviour
{
    public float rotateAngle;
    [SerializeField] private GameObject world;
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        world.transform.rotation = Quaternion.Euler(0f, rotateAngle, 0f);
        Vector3 tmpPos = playerTransform.position;
        playerTransform.position = Quaternion.Euler(0f, rotateAngle, 0f) * tmpPos ;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
