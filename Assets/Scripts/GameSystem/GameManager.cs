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
    [SerializeField]UIManager manager;

    public bool playerIsFall = false;
    public bool playerIsGoal = false;

    public bool playerIsGoalTutriale = false;

    public bool checkPoint_1 = false;
    public bool checkPoint_2 = false;
    public bool checkPoint_3 = false;

    public float heightOfFallDetection = -10;
    public Vector3 playerPositionCheckPoint;�@//������ɖ߂���W
    private float maxHeightOfPlayer;

    //[SerializeField] private float offsetFallDetectionPlane = -10;


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

        //Player��������
        if (playerIsFall)
        {
            PlayerFall();
            playerIsFall = false;
        }
        //Player�S�[������
        if (playerIsGoal)
        {
            PlayerGoal();
            playerIsGoal = false;
        }


        //��ŏ�������
        if (playerIsGoalTutriale)
        {
            PlayerGoalTutorial();
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

    //Player�������̏���
    private void PlayerFall()
    {
        StartCoroutine("RestartCheckPoint");
    }

    //�S�[�����̏���
    private void PlayerGoal()
    {
        int goaltime = manager.nowtime;
        int heightscore = (int)(manager.now_height / manager.goal_height * 100);
        UIManager.SaveTimeAndScore(goaltime, heightscore); // �^�C���ƃX�R�A���L�^
        Debug.Log("�S�[���ɓ��B���܂����I �^�C��: " + goaltime + " �X�R�A: " + heightscore);
        // �����ŃS�[���B���̒ǉ����������s�ł��܂�
        List<float> bestTimes = UIManager.LoadBestTimes();
        List<int> scores = UIManager.LoadScores();
        for (int i = 0; i < bestTimes.Count; i++)
        {
            Debug.Log("Rank " + (i + 1) + ": �^�C�� - " + bestTimes[i] + " �X�R�A - " + scores[i]);
        }
        StartCoroutine("ChangeToResultScene");
    }

    //�S�[�����̏���(tutriale�p�A��ŏ��������܂�)
    private void PlayerGoalTutorial()
    {
        StartCoroutine("ChangeToStageSelectScene");
    }

    private IEnumerator RestartCheckPoint()
    {
        screenfade.FadeOn(0, 1, 1.0f); //�t�F�[�h�A�E�g
        yield return new WaitForSeconds(1.0f);
        player.transform.position = playerPositionCheckPoint; //�Z�[�u�|�C���g�ֈړ�
        yield return new WaitForSeconds(0.5f);
        screenfade.FadeOn(1, 0, 0.5f); //�t�F�[�h�C��
    }

    private IEnumerator ChangeToResultScene()
    {
        yield return new WaitForSeconds(2.0f);
        screenfade.FadeOn(0, 1, 1.5f); //�t�F�[�h�A�E�g
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("ResultScene");
    }

    private IEnumerator ChangeToStageSelectScene()
    {
        yield return new WaitForSeconds(2.0f);
        screenfade.FadeOn(0, 1, 1.5f); //�t�F�[�h�A�E�g
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("StageSelectScene");
    }
}
