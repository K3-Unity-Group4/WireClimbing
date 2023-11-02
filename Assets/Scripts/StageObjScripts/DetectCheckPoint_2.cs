using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCheckPoint_2 : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float heightOfFallDetection;
    [SerializeField] private Vector3 checkPointPosition;
    [SerializeField] private Vector3 checkPointRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!gameManager.checkPoint_2)
            {
                gameManager.checkPoint_2 = true;
                gameManager.heightOfFallDetection = heightOfFallDetection;
                gameManager.playerPositionCheckPoint = checkPointPosition;
                gameManager.playerRotationCheckPoint = checkPointRotation;
                this.gameObject.SetActive(false);
            }
        }
    }
}
