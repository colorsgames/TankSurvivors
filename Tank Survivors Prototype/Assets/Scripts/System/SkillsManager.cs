using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance;

    public static UnityEvent StartGame = new UnityEvent();

    public float AllyHeath { get { return currentAllyHealth; } }
    public float AllySpawnRate { get { return currentAllySpawnRate; } }
    public float PlayerHealth { get { return currentPlayerHealth; } }
    public float PlayerDamage { get { return currentPlayerDamage; } }
    public float ShotDelay { get { return currentShotDelay; } }
    public float Speed { get { return currentSpeed; } }

    [SerializeField] private SpawnAliveEntityData allySpawnData;
    [SerializeField] private AliveEntityData ally;
    [SerializeField] private AliveEntityData player;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private SkillLevelData[] tempSkills;

    [SerializeField] private int allyCount = 0;
    [SerializeField] private float allyHealth = 1000;
    [SerializeField] private float allySpawnRate = 30;
    [SerializeField] private float playerHealth = 1000;
    [SerializeField] private float playerDamage = 20;
    [SerializeField] private float shotDelay = 0.5f;
    [SerializeField] private float speed = 500;

    float currentAllyHealth;
    float currentAllySpawnRate;
    float currentPlayerHealth;
    float currentPlayerDamage;
    float currentShotDelay;
    float currentSpeed;

    private void Awake()
    {
        Instance = this;

        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetFloat("allyHealth") > 0)
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
            currentSpeed = speed;
    }

    public void SetSkills()
    {
        StartGame.Invoke();
        foreach (var item in tempSkills)
        {
            item.ResetValue();
        }
        allySpawnData.maxCount = allyCount;
        weaponData.minDamage = currentPlayerDamage;
        weaponData.delay = currentShotDelay;
        ally.maxHealth = currentAllyHealth;
        player.maxHealth = currentPlayerHealth;
        allySpawnData.spawnRate = currentAllySpawnRate;
        ally.moveSpeed = currentSpeed + 100;
        player.moveSpeed = currentSpeed;

    }

    public void IncreaseAllyHealth(float value)
    {
        currentAllyHealth += value;
        PlayerPrefs.SetFloat("allyHealth", currentAllyHealth);
    }

    public void IncreaseDamage(float value)
    {
        currentPlayerDamage += value;
        PlayerPrefs.SetFloat("damage", currentPlayerDamage);
    }

    public void IncreaseHealth(float value)
    {
        currentPlayerHealth += value;
        PlayerPrefs.SetFloat("health", currentPlayerHealth);
    }

    public void UpSpeed(float value)
    {
        currentSpeed += value;
        PlayerPrefs.SetFloat("speed", currentSpeed);
    }

    public void DecreaseDelay(float value)
    {
        currentShotDelay -= value;
        PlayerPrefs.SetFloat("delay", currentShotDelay);
    }

    public void DecreaseAllySpawnRate(float value)
    {
        currentAllySpawnRate -= value;
        PlayerPrefs.SetFloat("allySpawnRate", currentAllySpawnRate);
    }
}
