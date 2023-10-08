using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : SelectButton
{
    //[SerializeField] private GameObject screen;
    //画像(動画)データリスト
    [SerializeField] private List<GameObject> screen_objects = new List<GameObject>();
    List<Renderer> screen_images=new List<Renderer>();
    protected override void AddStartProcess()
    {
        for (int i=0;i<screen_objects.Count;i++)
        {
            screen_images.Add(screen_objects[i].GetComponent<Renderer>());
            screen_images[i].enabled =i==0?true: false;
        }
    }
    protected override void AddProcess()
    {
        screen_images[base.focus_old - 1].enabled = false;
        screen_images[base.focus_now - 1].enabled = true;
    }
}
