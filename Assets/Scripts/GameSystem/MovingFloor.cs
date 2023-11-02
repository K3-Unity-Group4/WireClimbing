using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTiggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //gameObject.transform.parent = col.gameObject.transform;
            //col.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //gameObject.transform.parent = null;
        }
    }
}
