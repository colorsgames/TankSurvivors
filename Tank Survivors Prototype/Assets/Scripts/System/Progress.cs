using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public static Progress Instance;

    public ProgressInfo progressInfo;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            LoadExtern();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string js = JsonUtility.ToJson(progressInfo);
        SaveExtern(js);
    }

    public void SetProgressInfo(string value)
    {
        progressInfo = JsonUtility.FromJson<ProgressInfo>(value);
    }
}

[Serializable]
public class ProgressInfo
{
    public float currentAllyHealth = 1000;
    public float currentAllySpawnRate = 30;
    public float currentPlayerHealth = 1000;
    public float currentPlayerDamage = 20;
    public float currentShotDelay = 0.5f;
    public float currentSpeed = 500;

    public int totalMoney;

    public int levelSkill0;
    public int levelSkill1;
    public int levelSkill2;
    public int levelSkill3;
    public int levelSkill4;
    public int levelSkill5;

    public int priceSkill0 = 10;
    public int priceSkill1 = 10;
    public int priceSkill2 = 10;
    public int priceSkill3 = 10;
    public int priceSkill4 = 10;
    public int priceSkill5 = 10;

    public bool toggleState0;
    public bool toggleState1 = true;
}