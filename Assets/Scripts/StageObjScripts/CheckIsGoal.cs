using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsGoal : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //---�S�[�����̏���---
            Debug.Log("Goal");
        }
    }
}
