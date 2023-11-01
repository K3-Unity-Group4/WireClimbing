using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
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
