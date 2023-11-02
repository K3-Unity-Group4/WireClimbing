using UnityEngine;
using TMPro;


public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timetext;
    [SerializeField] TextMeshProUGUI heighttext;
  

    private void Start()
    {
        // GoalTime‚ðŽæ“¾
        float nowtime = UIManager.LoadNowTime();
        int nowheight = UIManager.LoadNowHeight();

        // GoalTime‚ð•\Ž¦
        timetext.text = "Time: " + nowtime.ToString() + " s";
        heighttext.text = "Height: " + nowtime.ToString() + " %";

    }
}
