using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ScoreData
{
    public List<float> bestTimes = new List<float>();
    public List<int> scores = new List<int>();
}
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
    [System.NonSerialized]public int now_height;
    [System.NonSerialized] public int goal_height;
    int original_height;
    float originalUI_pos;
    //時間
    public int nowtime;
    [Header("残り時間 10秒以上の整数")]
    [SerializeField] private int time_left; //残り時間
    private const int MaxBestTimes = 5;
    public static string prestagename;
    private void Start()
    {
        Time.timeScale = 1;
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
        prestagename= SceneManager.GetActiveScene().name;
        stage_text.text = prestagename;
        fasttime_text.text = PlayerPrefs.GetFloat("BestTime0").ToString()+" s";
        StartCoroutine("TimeManager");
    }
    void Update()
    {
        HeightManager();
        PauseManager();
    }
    void PauseManager()    //UI管理
    {
        if (pausecanvas.activeSelf == false && (Input.GetKeyDown(KeyCode.E)|| OVRInput.GetDown(OVRInput.Button.Two)) && iskey == true)   //escapeボタンでポーズ　クエストボタンにする
        {
            Time.timeScale = 0f;
            pausecanvas.SetActive(true);
            
        }
        else if (pausecanvas.activeSelf == true && (Input.GetKeyDown(KeyCode.E) || OVRInput.GetDown(OVRInput.Button.Two)) && iskey == true)  //マウス右ボタンでポーズを閉じる　クエストボタンにする
        {
            Time.timeScale = 1f;
            pausecanvas.SetActive(false);
            

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
            nowtime_text.text =  nowtime.ToString() + " s";
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
        
        height_text.text =  now_height.ToString() + " m";
        height_gage.fillAmount = (float)now_height / goal_height;
        //UI_pos.transform.position =new Vector3(UI_pos.position.x,originalUI_pos+500f* height_gage.fillAmount, 0);
    }

    public static void SaveTimeAndScore(float time, int score)
    {
        ScoreData scoreData = LoadData();
        scoreData.bestTimes.Add(time);
        scoreData.scores.Add(score);

        //Result用
        PlayerPrefs.SetFloat("NowTime", time);
        PlayerPrefs.SetInt("NowHeight", score);
        PlayerPrefs.Save();

        if (scoreData.bestTimes.Count > MaxBestTimes)
        {
            float maxTime = scoreData.bestTimes[0]; // 最初の要素を最大値と仮定
            int maxTimeIndex = 0;

            // リスト内の最大値を見つける
            for (int i = 1; i < scoreData.bestTimes.Count; i++)
            {
                if (scoreData.bestTimes[i] > maxTime)
                {
                    maxTime = scoreData.bestTimes[i];
                    maxTimeIndex = i;
                }
            }

            // 最も大きいタイムとそれに対応するスコアを削除
            scoreData.bestTimes.RemoveAt(maxTimeIndex);
            scoreData.scores.RemoveAt(maxTimeIndex);
        }

        SaveData(scoreData);
    }

    public static float LoadNowTime()
    {
        return PlayerPrefs.GetFloat("GoalTime", 0.0f);
    }

    public static int LoadNowHeight()
    {
        return PlayerPrefs.GetInt("NowHeight", 0);
    }

    public static List<float> LoadBestTimes()
    {
        ScoreData scoreData = LoadData();
        List<float> bestTimes = scoreData.bestTimes;
        bestTimes.Sort();
        return bestTimes;
    }

    public static List<int> LoadScores()
    {
        ScoreData scoreData = LoadData();
        List<int> scores = scoreData.scores;
        List<float> bestTimes = scoreData.bestTimes;

        // タイムを昇順にソート
        List<int> sortedScores = new List<int>();
        for (int i = 0; i < bestTimes.Count; i++)
        {
            int index = bestTimes.IndexOf(bestTimes[i]);
            sortedScores.Add(scores[index]);
        }

        return sortedScores;
    }

    private static ScoreData LoadData()
    {
        string json = PlayerPrefs.GetString("ScoreData", "");
        if (string.IsNullOrEmpty(json))
        {
            return new ScoreData();
        }
        return JsonUtility.FromJson<ScoreData>(json);
    }

    private static void SaveData(ScoreData scoreData)
    {
        string json = JsonUtility.ToJson(scoreData);
        PlayerPrefs.SetString("ScoreData", json);
        PlayerPrefs.Save();
    }

    public static void ResetData()
    {
        PlayerPrefs.DeleteKey("ScoreData");
        PlayerPrefs.Save();
    }

    public static void InitializeIfNecessary()
    {
        if (!PlayerPrefs.HasKey("ScoreData"))
        {
            // セーブデータがない場合、初期化
            ResetData();
        }
    }
}

