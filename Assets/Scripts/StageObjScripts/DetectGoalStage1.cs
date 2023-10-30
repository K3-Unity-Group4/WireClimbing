using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGoalStage1 : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Canvas goalText;
    // Start is called before the first frame update
    void Start()
    {
        goalText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!gameManager.playerIsGoal)
            {
                gameManager.playerIsGoal = true;
                goalText.enabled = true;
            }
        }
    }
}
