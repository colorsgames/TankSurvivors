using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance;

    public static UnityEvent StartGame = new UnityEvent();

    public float AllyHeath { get { return Progress.Instance.progressInfo.currentAllyHealth; } }
    public float AllySpawnRate { get { return Progress.Instance.progressInfo.currentAllySpawnRate; } }
    public float PlayerHealth { get { return Progress.Instance.progressInfo.currentPlayerHealth; } }
    public float PlayerDamage { get { return Progress.Instance.progressInfo.currentPlayerDamage; } }
    public float ShotDelay { get { return Progress.Instance.progressInfo.currentShotDelay; } }
    public float Speed { get { return Progress.Instance.progressInfo.currentSpeed; } }

    [SerializeField] private SpawnAliveEntityData allySpawnData;
    [SerializeField] private AliveEntityData ally;
    [SerializeField] private AliveEntityData player;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private SkillLevelData[] tempSkills;

    /*[SerializeField] private int allyCount = 0;
    [SerializeField] private float allyHealth = 1000;
    [SerializeField] private float allySpawnRate = 30;
    [SerializeField] private float playerHealth = 1000;
    [SerializeField] private float playerDamage = 20;
    [SerializeField] private float shotDelay = 0.5f;
    [SerializeField] private float speed = 500;*/

/*    float currentAllyHealth;
    float currentAllySpawnRate;
    float currentPlayerHealth;
    float currentPlayerDamage;
    float currentShotDelay;
    float currentSpeed;*/

    private void Awake()
    {
        Instance = this;

        //PlayerPrefs.DeleteAll();
        /*if (PlayerPrefs.GetFloat("allyHealth") > 0)
            currentAllyHealth = PlayerPrefs.GetFloat("allyHealth");
        else
            currentAllyHealth = allyHealth;
        if (PlayerPrefs.GetFloat("damage") > 0)
            currentPlayerDamage = PlayerPrefs.GetFloat("damage");
        else
            currentPlayerDamage = playerDamage;
        if (PlayerPrefs.GetFloat("delay") > 0)
            currentShotDelay = PlayerPrefs.GetFloat("delay");
        else
            currentShotDelay = shotDelay;
        if (PlayerPrefs.GetFloat("health") > 0)
            currentPlayerHealth = PlayerPrefs.GetFloat("health");
        else
            currentPlayerHealth = playerHealth;
        if (PlayerPrefs.GetFloat("allySpawnRate") > 0)
            currentAllySpawnRate = PlayerPrefs.GetFloat("allySpawnRate");
        else
            currentAllySpawnRate = allySpawnRate;
        if (PlayerPrefs.GetFloat("speed") > 0)
            currentSpeed = PlayerPrefs.GetFloat("speed");
        else
            currentSpeed = speed;*/
    }

    public void SetSkills()
    {
        StartGame.Invoke();
        foreach (var item in tempSkills)
        {
            item.ResetValue();
        }
        allySpawnData.maxCount = 0;
        weaponData.minDamage = Progress.Instance.progressInfo.currentPlayerDamage;
        weaponData.delay = Progress.Instance.progressInfo.currentShotDelay;
        ally.maxHealth = Progress.Instance.progressInfo.currentAllyHealth;
        player.maxHealth = Progress.Instance.progressInfo.currentPlayerHealth;
        allySpawnData.spawnRate = Progress.Instance.progressInfo.currentAllySpawnRate;
        ally.moveSpeed = Progress.Instance.progressInfo.currentSpeed + 100;
        player.moveSpeed = Progress.Instance.progressInfo.currentSpeed;

    }

    public void IncreaseAllyHealth(float value)
    {
        Progress.Instance.progressInfo.currentAllyHealth += value;
#if UNITY_WEBGL
        Progress.Instance.Save();
#endif
        //PlayerPrefs.SetFloat("allyHealth", currentAllyHealth);
    }

    public void IncreaseDamage(float value)
    {
        Progress.Instance.progressInfo.currentPlayerDamage += value;
#if UNITY_WEBGL
        Progress.Instance.Save();
#endif
        //PlayerPrefs.SetFloat("damage", currentPlayerDamage);
    }

    public void IncreaseHealth(float value)
    {
        Progress.Instance.progressInfo.currentPlayerHealth += value;
#if UNITY_WEBGL
        Progress.Instance.Save();
#endif
        //PlayerPrefs.SetFloat("health", currentPlayerHealth);
    }

    public void UpSpeed(float value)
    {
        Progress.Instance.progressInfo.currentSpeed += value;
#if UNITY_WEBGL
        Progress.Instance.Save();
#endif
        //PlayerPrefs.SetFloat("speed", currentSpeed);
    }

    public void DecreaseDelay(float value)
    {
        Progress.Instance.progressInfo.currentShotDelay -= value;
#if UNITY_WEBGL
        Progress.Instance.Save();
#endif
        //PlayerPrefs.SetFloat("delay", currentShotDelay);
    }

    public void DecreaseAllySpawnRate(float value)
    {
        Progress.Instance.progressInfo.currentAllySpawnRate -= value;
#if UNITY_WEBGL
        Progress.Instance.Save();
#endif
        //PlayerPrefs.SetFloat("allySpawnRate", currentAllySpawnRate);
    }
}
