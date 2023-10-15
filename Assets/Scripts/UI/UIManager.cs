using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausecanvas;
    //[SerializeField] private ScoreManager sm;
    [SerializeField] private TextMeshProUGUI stage_text;
    [SerializeField] private TextMeshProUGUI height_text;
    [SerializeField] private TextMeshProUGUI nowtime_text;
    [SerializeField] private TextMeshProUGUI fasttime_text;
    [SerializeField] private Image height_gage;
    //[SerializeField] private GameObject finishtext; //終わりテキスト
    bool iskey; //現在キー操作可能か
    [SerializeField] GameObject bottom_object;
    [SerializeField] GameObject goal_object;
    [SerializeField] Transform player_pos;
    [SerializeField] Transform UI_pos;

    //高さ
    int now_height;
    int goal_height;
    int original_height;
    float originalUI_pos;
    //時間
    int nowtime;
    [Header("残り時間 10秒以上の整数")]
    [SerializeField] private int time_left; //残り時間
    private void Start()
    {
        //if (pausecanvas.activeSelf == true) pausecanvas.SetActive(false);
        //if (finishtext.activeSelf == true) finishtext.SetActive(false);
        //if (time_left < 10) time_left = 30;
        iskey = true;
        now_height = 0;
        nowtime = 0;
        goal_height = (int)goal_object.transform.position.y;
        original_height = (int)bottom_object.transform.position.y;
        height_gage.fillAmount = 0;
        //originalUI_pos = UI_pos.position.y;
        stage_text.text = SceneManager.GetActiveScene().name;
        StartCoroutine("TimeManager");
    }
    void Update()
    {
        HeightManager();
        PauseManager();
    }
    void PauseManager()    //UI管理
    {
        if (Input.GetKey(KeyCode.E) && iskey == true)   //escapeボタンでポーズ　クエストボタンにする
        {
            Time.timeScale = 0f;
            pausecanvas.SetActive(true);
        }
        if (pausecanvas.activeSelf == true && Input.GetMouseButton(0) && iskey == true)  //マウス右ボタンでポーズを閉じる　クエストボタンにする
        {
            Time.timeScale = 1f;
            pausecanvas.SetActive(false);
        }
        if (pausecanvas.activeSelf == true && Input.GetMouseButton(1) && iskey == true)  //マウス左ボタンでポーズからタイトル　クエストボタンにする
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScene");
        }

        //if (time_left <= 0)
        //{
        //    Time.timeScale = 0f;
        //    iskey = false;
        //    StartCoroutine("Finish");
        //}
    }

    IEnumerator TimeManager()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            time_left -= 1;
            nowtime+=1;
            nowtime_text.text = "Time:" + nowtime.ToString() + "s";
            Debug.Log(time_left);
        }
    }

    //IEnumerator Finish()    //ゲーム終了時のテキスト表示
    //{
    //    finishtext.SetActive(true);
    //    finishtext.transform.Rotate(0, 0, 45 / 30f);
    //    yield return new WaitForSecondsRealtime(2f);
    //    SceneManager.LoadScene("ResultScene");
    //}
    void HeightManager()
    {
        now_height = (int)player_pos.position.y - original_height;
        
        height_text.text = "Height:" + now_height.ToString() + " m";
        height_gage.fillAmount = (float)now_height / goal_height;
        //UI_pos.transform.position =new Vector3(UI_pos.position.x,originalUI_pos+500f* height_gage.fillAmount, 0);
    }
}