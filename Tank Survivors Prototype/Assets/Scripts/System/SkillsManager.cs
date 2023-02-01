using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager instance;

    [SerializeField] private SpawnAliveEntityData allySpawnData;
    [SerializeField] private AliveEntityData ally;
    [SerializeField] private AliveEntityData player;
    [SerializeField] private WeaponData weaponData;

    [SerializeField] private int allyCount;
    [SerializeField] private int allyHealth = 1000;
    [SerializeField] private int allySpawnRate = 30;
    [SerializeField] private int playerHealth = 1000;
    [SerializeField] private int damage = 20;
    [SerializeField] private float shotDelay = 0.5f;
    [SerializeField] private float speed = 500;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        allyCount = PlayerPrefs.GetInt("allyCount");
        if (PlayerPrefs.GetInt("allyHealth") > 0)
            allyHealth = PlayerPrefs.GetInt("allyHealth");
        if (PlayerPrefs.GetInt("damage") > 0)
            damage = PlayerPrefs.GetInt("damage");
        if (PlayerPrefs.GetFloat("delay") > 0)
            shotDelay = PlayerPrefs.GetFloat("delay");
        if (PlayerPrefs.GetInt("health") > 0)
            playerHealth = PlayerPrefs.GetInt("health");
        if (PlayerPrefs.GetInt("allySpawnRate") > 0)
            allySpawnRate = PlayerPrefs.GetInt("allySpawnRate");
        if (PlayerPrefs.GetInt("speed") > 0)
            speed = PlayerPrefs.GetFloat("speed");
    }

    public void SetSkills()
    {
        allySpawnData.maxCount = allyCount;
        weaponData.minDamage = damage;
        weaponData.delay = shotDelay;
        ally.maxHealth = allyHealth;
        player.maxHealth = playerHealth;
        allySpawnData.spawnRate = allySpawnRate;
        ally.moveSpeed = speed + 100;
        player.moveSpeed = speed;

    }

    public void IncreaseAllyCount()
    {
        allyCount++;
        PlayerPrefs.SetInt("allyCount", allyCount);
    }

    public void IncreaseAllyHealth(int value)
    {
        allyHealth += value;
        PlayerPrefs.SetInt("allyHealth", allyHealth);
    }

    public void IncreaseDamage(int value)
    {
        damage += value;
        PlayerPrefs.SetInt("damage", damage);
    }

    public void IncreaseHealth(int value)
    {
        playerHealth += value;
        PlayerPrefs.SetInt("health", playerHealth);
    }

    public void UpSpeed(float value)
    {
        speed += value;
        PlayerPrefs.SetFloat("speed", speed);
    }

    public void DecreaseDelay(float value)
    {
        shotDelay -= value;
        PlayerPrefs.SetFloat("delay", shotDelay);
    }

    public void DecreaseAllySpawnRate(int value)
    {
        allySpawnRate -= value;
        PlayerPrefs.SetInt("allySpawnRate", allySpawnRate);
    }
}
