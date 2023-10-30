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
                //---ゴール時の処理---
                goalText.enabled = true;
                StartCoroutine("WaitChangeScene");

                //Debug.Log("Goal");

            }
        }
    }

    private IEnumerator WaitChangeScene()
    {
        //1秒待つ
        yield return new WaitForSeconds(2.0f);
    }
}
