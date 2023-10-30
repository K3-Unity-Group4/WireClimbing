using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]private GameObject player;
    [SerializeField] private GameObject fallDetectionPlane;
    [SerializeField] private OVRScreenFade screenfade;

    public bool playerIsFall = false;
    public bool playerIsGoal = false;

    public bool playerIsGoalTutriale = false;

    public bool checkPoint_1 = false;

    public float heightOfFallDetection = -10;
    public Vector3 playerPositionCheckPoint;�@//������ɖ߂���W
    private float maxHeightOfPlayer;

    //[SerializeField] private float offsetFallDetectionPlane = -10;


    // Start is called before the first frame update
    void Start()
    {
        playerPositionCheckPoint = new Vector3(0f, 1.26f, 0f);
        maxHeightOfPlayer = player.transform.position.y;
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

        if (playerIsGoal)
        {
            PlayerGoal();
            playerIsGoal = false;
        }


        //��ŏ�������
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

    //Player�������̏���
    private void PlayerFall()
    {
        screenfade.FadeOn(0, 1); //�t�F�[�h�A�E�g
        StartCoroutine("WaitChangeScene", 2.0f);
        player.transform.position = playerPositionCheckPoint; //�Z�[�u�|�C���g�ֈړ�
        StartCoroutine("WaitChangeScene", 3.0f);
        screenfade.FadeOn(1, 0); //�t�F�[�h�C��
    }

    //�S�[�����̏���
    private void PlayerGoal()
    {
        StartCoroutine("WaitChangeScene", 2.0f);
        screenfade.FadeOn(0, 1); //�t�F�[�h�A�E�g
        SceneManager.LoadScene("RankingScene");

    }

    //�S�[�����̏���(tutriale�p�A��ŏ��������܂�)
    private void PlayerGoalTutriale()
    {
        StartCoroutine("WaitChangeScene", 2.0f);
        screenfade.FadeOn(0, 1); //�t�F�[�h�A�E�g
        SceneManager.LoadScene("StageSelectScene");

    }

    private IEnumerator WaitChangeScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
