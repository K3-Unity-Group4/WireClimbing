using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveImage : MonoBehaviour
{
    private int radius = 1000;
    private int underHeight = -100;
    
    void Start()
    {
        Move();
    }

    void Move()
    {
        int angle = Random.Range(0, 361);
        transform.DOPath(
            new[]
            {
                new Vector3(Mathf.Cos(angle) * radius/2, underHeight/3, Mathf.Sin(angle) * radius/2),
                new Vector3(Mathf.Cos(angle) * radius, underHeight, Mathf.Sin(angle) * radius)
            },
            3f, PathType.Linear);
        transform.DOMove(new Vector3(Mathf.Cos(angle) * radius, underHeight, Mathf.Sin(angle) * radius), 10f);
    }
}
