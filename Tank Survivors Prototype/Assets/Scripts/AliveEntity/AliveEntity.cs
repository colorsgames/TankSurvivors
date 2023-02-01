using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AliveEntity : MonoBehaviour
{
    public bool Alive { get { return alive; } set { alive = value; } }
    public int Health { get { return currentHealth; } }
    public Rigidbody2D Rb { get { return rb; } }
    [HideInInspector]public Tower tower;
    /*    [SerializeField] private int startWeaponId;
        [SerializeField] private int maxHealth;

        [SerializeField] private GameObject explosionPrefab;

        [SerializeField] protected WeaponData weaponData;

        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed = 20;*/
    [SerializeField] protected AliveEntityData aliveEntityData;

    protected WeaponData weaponData;

    protected int MaxWeaponId
    {
        get { return maxWeaponId; }
        private set { maxWeaponId = value; }
    }

    protected int currentWeaponId;
    protected Weapons weapons;
    protected Vector2 movementInput;

    int maxHealth;
    int maxWeaponId;

    float speed;
    float rotationSpeed;

    Rigidbody2D rb;

    Vector2 velocity;

    int currentHealth;

    bool alive = true;

    public virtual void Awake()
    {
        tower = GetComponentInChildren<Tower>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = aliveEntityData.maxHealth;
        speed = aliveEntityData.moveSpeed;
        rotationSpeed = aliveEntityData.rotationSpeed;
        currentHealth = maxHealth;
        weaponData = aliveEntityData.weaponData;
        currentWeaponId = aliveEntityData.startWeaponId;
    }

    public virtual void Start()
    {
        MaxWeaponId = tower.transform.childCount - 1;
        ChangeWeapon(aliveEntityData.startWeaponId);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public WeaponData GetWeaponData()
    {
        return weaponData;
    }

    public virtual void MakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (alive) Dead();
        }
    }

    public virtual void Dead()
    {
        Instantiate(aliveEntityData.explosionPrefab, transform.position, quaternion.identity);
        alive = false;
    }

    public virtual void Healing(int value)
    {
        if (currentHealth == maxHealth) return;
        if (currentHealth + value >= maxHealth)
        {
            PopupTextManager.instance.SpawnDamageText(transform.position, ColorType.healing, $"+{maxHealth - currentHealth}");
            currentHealth = maxHealth;
        }
        else
        {
            PopupTextManager.instance.SpawnDamageText(transform.position, ColorType.healing, $"+{value}");
            currentHealth += value;
        }
    }

    protected void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void ChangeWeapon(int id)
    {
        for (int i = 0; i < tower.transform.childCount; i++)
        {
            if(id == i)
            {
                tower.transform.GetChild(i).gameObject.SetActive(true);
                weapons = tower.transform.GetChild(i).GetComponent<Weapons>();
            }
            else
            {
                tower.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    void Movement()
    {
        velocity = rb.velocity;

        movementInput = Vector2.ClampMagnitude(movementInput, 1);

        if (!alive) movementInput = Vector2.zero;

        velocity = (Vector2.up * movementInput.x * speed) +
            (Vector2.right * movementInput.y * speed);

        rb.velocity = velocity * Time.deltaTime;

        if (velocity != Vector2.zero)
        {
            Quaternion rot = Quaternion.Lerp(transform.localRotation, Quaternion.LookRotation(Vector3.forward, velocity), rotationSpeed * Time.deltaTime);
            rb.MoveRotation(rot);
        }
    }
}
