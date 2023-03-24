using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AliveEntity : MonoBehaviour
{
    public bool Alive { get { return alive; } set { alive = value; } }
    public float Health { get { return currentHealth; } }
    public Rigidbody2D Rb { get { return rb; } }

    [HideInInspector] public Tower tower;

    [SerializeField] protected AliveEntityData aliveEntityData;

    [SerializeField] AudioSource explosionSource;
    [SerializeField] AudioClip explosionClip;

    protected WeaponData weaponData;

    protected int MaxWeaponId
    {
        get { return maxWeaponId; }
        private set { maxWeaponId = value; }
    }

    protected int currentWeaponId;
    protected Weapons weapons;
    protected Vector2 movementInput;

    private AudioSource source;

    int maxWeaponId;

    float currentHealth;
    float rotationSpeed;

    Rigidbody2D rb;

    Vector2 velocity;


    bool alive = true;

    public virtual void Awake()
    {
        if (GetComponentInChildren<Tower>())
            tower = GetComponentInChildren<Tower>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        currentHealth = aliveEntityData.maxHealth;
        rotationSpeed = aliveEntityData.rotationSpeed;
        weaponData = aliveEntityData.weaponData;
        currentWeaponId = aliveEntityData.startWeaponId;
    }

    public virtual void Start()
    {
        if (tower)
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

    public virtual void MakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (alive) Destroy();
        }
    }

    public virtual void Destroy()
    {
        Dead();
    }

    public void Dead()
    {
        Instantiate(aliveEntityData.explosionPrefab, transform.position, quaternion.identity);
        explosionSource.PlayOneShot(explosionClip);
        alive = false;
    }

    public virtual void Healing(float value)
    {
        if (currentHealth == aliveEntityData.maxHealth) return;
        if (currentHealth + value >= aliveEntityData.maxHealth)
        {
            PopupTextManager.Instance.SpawnDamageText(transform.position, ColorType.healing, $"+{(aliveEntityData.maxHealth - currentHealth).ToString("0.0")}");
            currentHealth = aliveEntityData.maxHealth;
        }
        else
        {
            PopupTextManager.Instance.SpawnDamageText(transform.position, ColorType.healing, $"+{value.ToString("0.0")}");
            currentHealth += value;
        }
    }

    protected void ResetHealth()
    {
        currentHealth = aliveEntityData.maxHealth;
    }

    public void ChangeWeapon(int id)
    {
        if (id == -1) return;
        for (int i = 0; i < tower.transform.childCount; i++)
        {
            if (id == i)
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

    public float GetSpeed()
    {
        return rb.velocity.magnitude;
    }

    void Movement()
    {
        velocity = rb.velocity;

        movementInput = Vector2.ClampMagnitude(movementInput, 1);

        if (!source.isPlaying)
            source.Play();
        source.pitch = 1 + movementInput.magnitude;

        if (!alive) movementInput = Vector2.zero;

        velocity = (Vector2.up * movementInput.x * aliveEntityData.moveSpeed) +
            (Vector2.right * movementInput.y * aliveEntityData.moveSpeed);

        rb.velocity = velocity * Time.deltaTime;

        if (velocity != Vector2.zero)
        {
            Quaternion rot = Quaternion.Lerp(transform.localRotation, Quaternion.LookRotation(Vector3.forward, velocity), rotationSpeed * Time.deltaTime);
            rb.MoveRotation(rot);
        }
    }
}
