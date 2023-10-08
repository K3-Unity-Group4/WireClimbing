using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CraateImage : MonoBehaviour
{
    [SerializeField] private GameObject yae;
    [SerializeField] private GameObject kokusei;
    [SerializeField] private GameObject yelan;
    [SerializeField] private GameObject nahida;
    [SerializeField] private GameObject eulua;
    [SerializeField] private GameObject hutao;
    [SerializeField] private GameObject raiden;
    [SerializeField] private GameObject sholi;
    private GameObject[] image; 
    private double time = 0;

    private void Start()
    {
        image = new[] {yae, kokusei, yelan, nahida, eulua, hutao, raiden, sholi};
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            time = 0;
            CreateImage();
        }
    }

    void CreateImage()
    {
        GameObject obj = RandomImage();
        Instantiate(obj, transform.position, Quaternion.identity);
    }

    GameObject RandomImage()
    {
        int random = Random.Range(0, 8);
        return image[random];
    }
}
