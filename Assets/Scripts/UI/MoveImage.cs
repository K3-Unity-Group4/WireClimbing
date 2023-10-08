using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveImage : MonoBehaviour
{
    private int radius = 1000;
    private int underHeight = -100;
    private Transform lookAt;
    
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        lookAt = obj.GetComponent<Transform>();
        Move();
    }

    private void Update()
    {
        transform.LookAt(lookAt);
    }

    void Move()
    {
        int angle = Random.Range(0, 361);
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOPath(
                new[]
                {
                    new Vector3(Mathf.Cos(angle) * radius/2, 300, Mathf.Sin(angle) * radius/2),
                    new Vector3(Mathf.Cos(angle) * radius, underHeight, Mathf.Sin(angle) * radius)
                },
                10f, PathType.Linear))
            .OnComplete(() => {Destroy(gameObject);})
        ;
        
    }
}
