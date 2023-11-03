using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class RankingManager : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> timetexts;
    [SerializeField] List<TextMeshProUGUI> heighttexts;
    private float resetTime = 0;

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

    private void Update()
    {
        // ランキング削除
        if (OVRInput.Get(OVRInput.RawButton.LThumbstick) && OVRInput.Get(OVRInput.RawButton.LThumbstick))
        {
            resetTime += Time.deltaTime;
            if (resetTime >= 20)
            {
                UIManager.ResetData();
                resetTime = 0;
            }

            if (OVRInput.GetUp(OVRInput.RawButton.LThumbstick) || OVRInput.GetUp(OVRInput.RawButton.LThumbstick)) resetTime = 0;
        }
    }
}
