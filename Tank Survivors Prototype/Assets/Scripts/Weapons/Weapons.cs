using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapons : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private AudioSource source;

    public bool Shooting { get { return shooting; } }

    private ObjectPool<Shell> pool;

    AliveEntity owner;

    Tower tower;

    WeaponData weaponData;

    float currentTime;

    bool shooting;
    bool canShoot;

    private void Awake()
    {
        tower = transform.parent.GetComponent<Tower>();
        owner = tower.GetParent();
        weaponData = owner.GetWeaponData();
        currentTime = weaponData.delay;

        pool = new ObjectPool<Shell>(CreateShell, OnTakeShell, OnReturnShell);
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
            source.PlayOneShot(weaponData.shot);

            for (int i = 0; i < spawnPoint.Length; i++)
            {
                GetShell(i);
            }
            //CinemachineShake.Instance.StartShake(weaponData.shotShakeIntensity, weaponData.shotShakeTime);
        }
    }

    void OnTakeShell(Shell shell)
    {
        //shell.transform.position = item.transform.position;
        shell.SetStartPos(transform.position);
        shell.SetCreatorVelocity(owner.GetVelocity());
        float minDamage = weaponData.minDamage;
        shell.SetDamage(Random.Range(minDamage, minDamage + weaponData.maxDamage));
        shell.HideObject(false);
        shooting = false;
        canShoot = false;
        currentTime = 0;
    }

    void OnReturnShell(Shell shell)
    {
        shell.HideObject(true);
    }

    void GetShell(int i)
    {
        var shell = pool.Get();
        shell.transform.position = spawnPoint[i].position;
        shell.transform.rotation = spawnPoint[i].rotation;
        shell.SetDir(spawnPoint[i].right);
    }

    Shell CreateShell()
    {
        Shell shell = Instantiate(weaponData.shell);
        shell.SetPool(pool);
        return shell;
    }
}
