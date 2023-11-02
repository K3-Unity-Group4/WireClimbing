using UnityEngine;
using TMPro;


public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timetext;
    [SerializeField] TextMeshProUGUI heighttext;
  

    private void Start()
    {
        // GoalTime���擾
        float nowtime = UIManager.LoadNowTime();
        int nowheight = UIManager.LoadNowHeight();

        // GoalTime��\��
        timetext.text = "Time: " + nowtime.ToString() + " s";
        heighttext.text = "Height: " + nowtime.ToString() + " %";

    }
}
