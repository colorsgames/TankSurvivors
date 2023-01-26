using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;

    public bool Shooting { get { return shooting; } }

    AliveEntity owner;

    Tower tower;

    WeaponData weaponData;

    float currentTime;

    bool shooting;
    bool canShoot;

    private void Start()
    {
        tower = transform.parent.GetComponent<Tower>();
        owner = tower.GetParent();
        weaponData = owner.GetWeaponData();
        currentTime = weaponData.delay;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= weaponData.delay)
        {
            canShoot = true;
        }
    }

    public void StartShooting()
    {
        if (canShoot)
        {
            Shot();
        }
    }

    void Shot()
    {
        foreach (var item in spawnPoint)
        {
            Shell instShell = Instantiate(weaponData.shell, item.position, Quaternion.identity);
            instShell.SetCreatorVelocity(owner.GetVelocity());
            instShell.SetDir(item.right);
            instShell.SetDamage(Random.Range(weaponData.minDamage, weaponData.maxDamage));
            shooting = false;
            canShoot = false;
            currentTime = 0;
        }

    }
}
