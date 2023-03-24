using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ADManager : MonoBehaviour
{
    public static ADManager Instance;

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    [SerializeField] private int maxAdvStep;

    int currentStep;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentStep = maxAdvStep;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowAD()
    {
        currentStep++;
        if(currentStep >= maxAdvStep)
        {
            currentStep = 0;
            ShowAdv();
        }
    }
}
