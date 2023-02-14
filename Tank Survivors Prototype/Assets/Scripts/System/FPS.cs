using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    TMP_Text fpsTMP;

    float fps;

    private void Awake()
    {
        fpsTMP = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        fps = 1.0f / Time.deltaTime;
        fpsTMP.text = "FPS: " + (int)fps;
    }
}
