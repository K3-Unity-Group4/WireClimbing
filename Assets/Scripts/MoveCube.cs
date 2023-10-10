using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Sequence = Oculus.Interaction.PoseDetection.Sequence;

public class MoveCube : MonoBehaviour
{
    private bool pos;
    private Vector3 target;
    Vector3 target1 = new Vector3(-20, 10, -10);
    Vector3 target2 = new Vector3(-20, 10, 10);

    private void Start()
    {
        target = target1;
    }

    private void Update()
    {
        Vector3 current = transform.position;
        
        if (Vector3.Distance(current, target1) <= 0) target = target2;
        else if (Vector3.Distance(current, target2) <= 0) target = target1;
        float step = 2.0f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(current, target, step);
    }
}
