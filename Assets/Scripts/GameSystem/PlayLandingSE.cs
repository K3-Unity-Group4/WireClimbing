using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLandingSE : MonoBehaviour
{

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SETest");
        //audioSource.Play();
    }
}
