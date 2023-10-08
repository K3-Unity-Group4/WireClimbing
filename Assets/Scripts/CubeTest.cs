using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ui;
    
    public void OnPointerClick()
    {
        Debug.Log("Cubeをクリックしました");
        ui.text = "クリックされた";
    }
}
