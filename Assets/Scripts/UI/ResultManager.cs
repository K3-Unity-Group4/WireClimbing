using UnityEngine;
using TMPro;


public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timetext;
    [SerializeField] TextMeshProUGUI heighttext;
  

    private void Start()
    {
        // GoalTimeを取得
        float nowtime = UIManager.LoadNowTime();
        int nowheight = UIManager.LoadNowHeight();

        // GoalTimeを表示
        timetext.text = "Time: " + nowtime.ToString() + " s";
        heighttext.text = "Height: " + nowtime.ToString() + " %";

    }
}
