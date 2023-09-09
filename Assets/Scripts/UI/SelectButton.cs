using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    [SerializeField] GameObject focus;
    [SerializeField] List<GameObject> select_texts=new List<GameObject>();
    private int focus_max;
    private int focus_now;
    // Start is called before the first frame update
    void Start()
    {
        focus_max = select_texts.Count;
        Debug.Log (focus_max);
        focus_now = 1;

    }

    // Update is called once per frame
    void Update()
    {
        //Joyスティックに変更予定
        if (Input.GetKeyDown(KeyCode.A))
        {
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
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
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
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (select_texts[focus_now - 1].name)
            {
                case "StartText":
                    SceneManager.LoadScene("Wire");
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
            }
        }
    }
}
