using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]private GameObject player;
    [SerializeField] private GameObject fallDetectionPlane;
    [SerializeField] private OVRScreenFade screenfade;
    [SerializeField] private float fadetime;


    public bool playerIsFall = false;
    public bool playerIsGoal = false;

    public bool playerIsGoalTutriale = false;

    public bool checkPoint_1 = false;
    public bool checkPoint_2 = false;
    public bool checkPoint_3 = false;

    public float heightOfFallDetection = -10;
    public Vector3 playerPositionCheckPoint;　//落下後に戻る座標
    private float maxHeightOfPlayer;

    //[SerializeField] private float offsetFallDetectionPlane = -10;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerPositionCheckPoint = new Vector3(0f, 1.26f, 0f);
        maxHeightOfPlayer = player.transform.position.y;
        screenfade.fadeTime = fadetime;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMaxheight();
        MoveFallDetectionPlane();

        //Player落下判定
        if (playerIsFall)
        {
            PlayerFall();
            playerIsFall = false;
        }
        //Playerゴール判定
        if (playerIsGoal)
        {
            PlayerGoal();
            playerIsGoal = false;
        }


        //後で書き直す
        if (playerIsGoalTutriale)
        {
            PlayerGoalTutriale();
            playerIsGoalTutriale = false;
        }
    }


    private void UpdateMaxheight()
    {
        float nowHeight = player.transform.position.y;
        if(nowHeight > maxHeightOfPlayer)
        {
            maxHeightOfPlayer = nowHeight;
        }
    }

    private void MoveFallDetectionPlane()
    {
        Vector3 pos = fallDetectionPlane.transform.position;
        Vector3 movedPosition = new Vector3(pos.x, heightOfFallDetection, pos.z);
        fallDetectionPlane.transform.position = movedPosition;
    }

    //Player落下時の処理
    private void PlayerFall()
    {
        StartCoroutine("RestartCheckPoint");
    }

    //ゴール時の処理
    private void PlayerGoal()
    {
        StartCoroutine("ChangeToRankingScene");
    }

    //ゴール時の処理(tutriale用、後で書き直します)
    private void PlayerGoalTutriale()
    {
        StartCoroutine("ChangeToStageSelectScene");
    }

    private IEnumerator RestartCheckPoint()
    {
        screenfade.FadeOn(0, 1, 1.0f); //フェードアウト
        yield return new WaitForSeconds(1.0f);
        player.transform.position = playerPositionCheckPoint; //セーブポイントへ移動
        yield return new WaitForSeconds(0.5f);
        screenfade.FadeOn(1, 0, 0.5f); //フェードイン
    }

    private IEnumerator ChangeToRankingScene()
    {
        yield return new WaitForSeconds(2.0f);
        screenfade.FadeOn(0, 1, 1.5f); //フェードアウト
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("RankingScene");
    }

    private IEnumerator ChangeToStageSelectScene()
    {
        yield return new WaitForSeconds(2.0f);
        screenfade.FadeOn(0, 1, 1.5f); //フェードアウト
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("StageSelectScene");
    }
}
