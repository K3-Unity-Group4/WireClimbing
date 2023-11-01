using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGoalStage1 : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager manager;
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
            if (!gameManager.playerIsGoal && other.CompareTag("Player"))
            {
                gameManager.playerIsGoal = true;
                goalText.enabled = true;

                //スコア処理
                int goaltime = manager.nowtime;
                int heightscore = (int)(manager.now_height / manager.goal_height * 100);
                UIManager.SaveTimeAndScore(goaltime, heightscore); // タイムとスコアを記録
                Debug.Log("ゴールに到達しました！ タイム: " + goaltime + " スコア: " + heightscore);
                // ここでゴール達成の追加処理を実行できます
                List<float> bestTimes = UIManager.LoadBestTimes();
                List<int> scores = UIManager.LoadScores();

                for (int i = 0; i < bestTimes.Count; i++)
                {
                    Debug.Log("Rank " + (i + 1) + ": タイム - " + bestTimes[i] + " スコア - " + scores[i]);
                }
            }
        }
    }
}
