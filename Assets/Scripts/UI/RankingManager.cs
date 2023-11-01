using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class RankingManager : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> timetexts;
    [SerializeField] List<TextMeshProUGUI> heighttexts;

    private void Start()
    {
        // タイムとスコアのデータを読み込む
        List<float> bestTimes = UIManager.LoadBestTimes();
        List<int> scores = UIManager.LoadScores();
;
        for (int i = 0; i < bestTimes.Count; i++)
        {
            timetexts[i].text = bestTimes[i].ToString()+"s";
            heighttexts[i].text = scores[i].ToString()+"%";

        }

    }
}
