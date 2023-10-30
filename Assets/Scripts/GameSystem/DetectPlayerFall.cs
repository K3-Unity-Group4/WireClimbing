using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerFall : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
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
            if (!gameManager.playerIsFall)
            {
                gameManager.playerIsFall = true;
            }
        }
    }
}
