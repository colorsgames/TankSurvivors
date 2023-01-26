using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : AliveEntity
{
    public Vector2 TowerTarget { get { return towerTarget; } }
    public int AllyCount { get { return allyCount; } }

    bool[] bussyAllyPlace = new bool[4];

    HealthBarController healthBar;

    Camera cam;

    Vector2 towerTarget;

    int allyCount;
    int oldDamage;

    float oldDelay;


    public override void Start()
    {
        healthBar = FindObjectOfType<HealthBarController>();
        oldDamage = weaponData.minDamage;
        oldDelay = weaponData.delay;
        cam = Camera.main;
        base.Start();
    }

    private void Update()
    {
        if (!Alive) return;
        movementInput.y = Input.GetAxis("Horizontal");
        movementInput.x = Input.GetAxis("Vertical");

        towerTarget = cam.ScreenToWorldPoint(Input.mousePosition);
        tower.SetTarget(towerTarget);

        if (Input.GetMouseButton(0))
        {
            weapons.StartShooting();
        }
    }

    public override void MakeDamage(int damage)
    {
        base.MakeDamage(damage);
        healthBar.UpdateValue(Health);
    }

    public override void Healing(int value)
    {
        base.Healing(value);
        healthBar.UpdateValue(Health);
    }

    public override void Dead()
    {
        base.Dead();
        GameManager.instance.OpenResults(false);
    }

    public int GetFreePlaceID()
    {
        int id = -1;
        for (int i = 0; i < bussyAllyPlace.Length; i++)
        {
            if (!bussyAllyPlace[i])
            {
                bussyAllyPlace[i] = true;
                id = i;
                break;
            }
        }
        return id;
    }

    public void SetFreePlace(int id)
    {
        bussyAllyPlace[id] = false;
    }

    public void IncreaseAlly()
    {
        allyCount++;
    }

    public void DecreaseAlly()
    {
        allyCount--;
    }

    public void NextWeapon()
    {
        if (currentWeaponId < MaxWeaponId - 1)
        {
            ChangeWeapon(++currentWeaponId);
            //ResetWeaponData();
        }
    }

    public void PreviousWeapon()
    {
        if (currentWeaponId > 0)
        {
            ChangeWeapon(--currentWeaponId);
            //ResetWeaponData();
        }
    }

    public void DecreaseShotTime(float value)
    {
        if (weaponData.delay > weaponData.minDelay)
            weaponData.delay -= value;
    }

    void ResetWeaponData()
    {
        weaponData.minDamage = oldDamage;
        weaponData.delay = oldDelay;
    }
}
