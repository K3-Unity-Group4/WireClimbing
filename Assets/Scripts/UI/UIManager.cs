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
    //[SerializeField] private GameObject finishtext; //�I���e�L�X�g
    bool iskey; //���݃L�[����\��
    [SerializeField] GameObject bottom_object;
    [SerializeField] GameObject goal_object;
    [SerializeField] Transform player_pos;
    [SerializeField] Transform UI_pos;

    //����
    int now_height;
    int goal_height;
    int original_height;
    float originalUI_pos;
    //����
    int nowtime;
    [Header("�c�莞�� 10�b�ȏ�̐���")]
    [SerializeField] private int time_left; //�c�莞��
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
    void PauseManager()    //UI�Ǘ�
    {
        if (Input.GetKey(KeyCode.E) && iskey == true)   //escape�{�^���Ń|�[�Y�@�N�G�X�g�{�^���ɂ���
        {
            Time.timeScale = 0f;
            pausecanvas.SetActive(true);
        }
        if (pausecanvas.activeSelf == true && Input.GetMouseButton(0) && iskey == true)  //�}�E�X�E�{�^���Ń|�[�Y�����@�N�G�X�g�{�^���ɂ���
        {
            Time.timeScale = 1f;
            pausecanvas.SetActive(false);
        }
        if (pausecanvas.activeSelf == true && Input.GetMouseButton(1) && iskey == true)  //�}�E�X���{�^���Ń|�[�Y����^�C�g���@�N�G�X�g�{�^���ɂ���
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

    //IEnumerator Finish()    //�Q�[���I�����̃e�L�X�g�\��
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