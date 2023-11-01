using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class StarParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem star;
    Color blue = Color.blue;
    Color puaple = Color.magenta;
    Color gold = Color.yellow;
    private Color[] _colors;

    private float time;
    
    
    
    [Obsolete("Obsolete")]
    void Start()
    {
        star.startColor = Color.blue;
        _colors = new[] { blue, puaple, gold };
    }
    
    
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 10)
        {
            ChangeColor();
            time = 0;
        }
    }

    void ChangeColor()
    {
        int random = Random.Range(0, 3);
        star.startColor = _colors[random];
    }
}
