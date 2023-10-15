using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckIsGoal : MonoBehaviour
{
    private bool isGoal = false;
    [SerializeField] private Canvas goalText;

    void Start()
    {
        goalText.enabled = false; 
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isGoal)
            {
                isGoal = true;
                //---ÉSÅ[ÉãéûÇÃèàóù---
                goalText.enabled = true;
                StartCoroutine("WaitChangeScene");

                //Debug.Log("Goal");

            }
        }
    }

    private IEnumerator WaitChangeScene()
    {
        //1ïbë“Ç¬
        yield return new WaitForSeconds(2.0f);
    }
}
