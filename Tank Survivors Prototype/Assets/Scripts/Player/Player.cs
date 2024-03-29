using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : AliveEntity
{
    public static Player Instance;

    public Vector2 TowerTarget { get { return towerTarget; } }

    public Joystick TowerJS { get { return jsTower; } }

    bool[] bussyAllyPlace = new bool[4];

    [SerializeField] private Joystick jsMovement;
    [SerializeField] private Joystick jsTower;

    [SerializeField] private float aimDistance;
    [SerializeField] private LayerMask aimMask;

    [SerializeField] private float shotShakeIntensity;
    [SerializeField] private float shotShakeTime;
    [SerializeField] private float damageShakeIntensity;
    [SerializeField] private float damageShakeTime;
    [SerializeField] private float idleTowerTime;

    [SerializeField] ItemSpawnManager healingSpawn;

    HealthBarController healthBar;

    Camera cam;

    Vector2 towerTarget;
    Vector2 lookPos;

    float oldDamage;
    float oldDelay;
    float currentIdleTowerTime;

    public override void Awake()
    {
        Instance = this;
        base.Awake();
    }

    public override void Start()
    {
        healthBar = FindObjectOfType<HealthBarController>();
        //nextWeapon = GameObject.Find("NextWeaponSpawn").GetComponent<ItemSpawnManager>();
        //previousWeapon = GameObject.Find("PreviousWeaponSpawn").GetComponent<ItemSpawnManager>();
        //decreaseShotTime = GameObject.Find("DecreaseShotTimeSpawn").GetComponent<ItemSpawnManager>();

        oldDamage = weaponData.minDamage;
        oldDelay = weaponData.delay;
        weaponData.shotShakeIntensity = shotShakeIntensity;
        weaponData.shotShakeTime = shotShakeTime;
        cam = Camera.main;
        //healingSpawn.SetSpawning(false);
        //previousWeapon.SetSpawning(false);

        base.Start();
    }

    private void Update()
    {
        if (!Alive) return;

        if (!PlatformManager.Instance.IsMobile)
        {
            movementInput.y = Input.GetAxis("Horizontal");
            movementInput.x = Input.GetAxis("Vertical");

            towerTarget = cam.ScreenToWorldPoint(Input.mousePosition);
            tower.SetTarget(towerTarget);

            if (Input.GetMouseButton(0))
            {
                weapons.StartShooting();
            }
        }
        else
        {
            movementInput.y = jsMovement.Horizontal;
            movementInput.x = jsMovement.Vertical;

            if (jsTower.Shoot)
            {
                weapons.StartShooting();
                currentIdleTowerTime = idleTowerTime;
            }
        }
    }

    private void LateUpdate()
    {
        if (!PlatformManager.Instance.IsMobile) return;
        lookPos = new Vector2(jsTower.Horizontal, jsTower.Vertical);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, lookPos, aimDistance, aimMask);
        /*        if (hit)
                {
                    towerTarget = hit.collider.gameObject.transform.position;
                }
                else*/
        towerTarget = (lookPos * aimDistance) + (Vector2)transform.position;

        currentIdleTowerTime -= Time.deltaTime;
        if (currentIdleTowerTime <= 0)
            tower.SetTarget(transform.up + transform.position);
        else
            tower.SetTarget(towerTarget);
    }

    public override void MakeDamage(float damage)
    {
        base.MakeDamage(damage);
        healingSpawn.SetSpawning(true);
        healthBar.UpdateValue(Health);
        CinemachineShake.Instance.StartShake(damageShakeIntensity, damageShakeTime);
    }

    public override void Healing(float value)
    {
        base.Healing(value);
        healthBar.UpdateValue(Health);
        if (Health >= aliveEntityData.maxHealth)
            healingSpawn.SetSpawning(false);
    }

    public override void Destroy()
    {
        base.Destroy();
        GameManager.Instance.OpenResults();
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

    public void NextWeapon()
    {
        if (currentWeaponId < MaxWeaponId)
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

    private void OnDrawGizmos()
    {
        Vector2 lookPos = new Vector2(jsTower.Horizontal, jsTower.Vertical);
        Gizmos.DrawRay(transform.position, lookPos * aimDistance);
    }
}
