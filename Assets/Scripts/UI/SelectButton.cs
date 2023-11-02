using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    [SerializeField] GameObject focus;
    [SerializeField] List<GameObject> select_texts=new List<GameObject>();


    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip transfer_se;
    [SerializeField] AudioClip select_se;
    [SerializeField] private OVRScreenFade screenfade;


    private int focus_max;
    protected private int focus_now;
    protected private int focus_old;
    private List<Animator> texts_animator = new List<Animator>();
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        focus_max = select_texts.Count;
        focus_now = 1;
        foreach(GameObject selecttext in select_texts)
        {
            texts_animator.Add(selecttext.GetComponent<Animator>());
        }
        texts_animator[focus_now - 1].SetBool("IsSelect", true);
        AddStartProcess();
    }

    // Update is called once per frame
    void Update()
    {
        //Joyスティックに変更
        if (Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Button.Four))
        {
            texts_animator[focus_now - 1].SetBool("IsSelect", false);
            focus_old = focus_now;
            focus_now ++;
            if (focus_now <= 0)
            {
                focus_now = focus_max;
            }
            else if (focus_now > focus_max)
            {
                focus_now = 1;
            }
            focus.transform.localPosition = select_texts[focus_now - 1].transform.localPosition + new Vector3(0, 10, 0);
            texts_animator[focus_now - 1].SetBool("IsSelect", true);
            PlaySound(audioSource, transfer_se);
            AddProcess();
        }
        if (Input.GetKeyDown(KeyCode.B) || OVRInput.Get(OVRInput.Button.Three))
        {
            texts_animator[focus_now - 1].SetBool("IsSelect", false);
            focus_old = focus_now;
            focus_now--;
            if (focus_now <= 0)
            {
                focus_now = focus_max;
            }
            else if (focus_now > focus_max)
            {
                focus_now = 1;
            }
            focus.transform.localPosition = select_texts[focus_now - 1].transform.localPosition + new Vector3(0, 10, 0);
            texts_animator[focus_now - 1].SetBool("IsSelect", true);
            PlaySound(audioSource, transfer_se);
            AddProcess();
        }
        
        if (Input.GetKeyDown(KeyCode.Return) || OVRInput.Get(OVRInput.Button.One))
        {
            PlaySound(audioSource, select_se);
            StartCoroutine("PressAnimation");
        }
    }

    protected virtual IEnumerator PressAnimation()
    {
        texts_animator[focus_now - 1].SetBool("IsPress", true);
        screenfade.FadeOn(0, 1, 1.5f); //フェードアウト
        Time.timeScale = 1f;
        yield return new WaitForEndOfFrame();
        yield return new WaitWhile(() => texts_animator[focus_now-1].GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
        texts_animator[focus_now - 1].SetBool("IsPress", false);
        Time.timeScale = 0f;
        switch (select_texts[focus_now - 1].name)
        {
            case "StartText":
            case "StageText":
                SceneManager.LoadScene("StageSelectScene");
                break;
            case "QuitText":
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
                break;
            case "RankingText":
                SceneManager.LoadScene("RankingScene");
                Debug.Log("test");
                break;
            case "TitleText":
                SceneManager.LoadScene("TitleScene");
                break;
            case "Stage1Text":
                //SceneManager.LoadScene("Stage1Scene");
                SceneManager.LoadScene("Turtrial");
                //Debug.Log("Stage1");
                break;
            case "Stage2Text":
                SceneManager.LoadScene("Stage1");
                //Debug.Log("stage1");
                break;
            case "Stage3Text":
                //SceneManager.LoadScene("Stage3Scene");
                Debug.Log("stage3");
                break;
            case "RePlayText":
                SceneManager.LoadScene(UIManager.prestagename);
                break;
        }
    }

    protected virtual void AddStartProcess() { }
    protected virtual void AddProcess() { }

    private void PlaySound(AudioSource aus,AudioClip se)
    {
        aus.Stop(); // 既に再生中の場合、再生を停止
        aus.clip = se;
        aus.Play();
    }
}
