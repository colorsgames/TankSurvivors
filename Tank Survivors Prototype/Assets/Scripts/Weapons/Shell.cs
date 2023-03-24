using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shell : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;

    [SerializeField] private float deadRad;
    [SerializeField] private float speed;

    [SerializeField] private AudioClip sound;

    private IObjectPool<Shell> pool;

    private Rigidbody2D rb;

    private Vector2 creatorVelocity;
    private Vector2 direction;
    private Vector2 velocity;
    private Vector2 oldPos;

    private float damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        oldPos = transform.position;
    }

    private void FixedUpdate()
    {
        velocity = direction * speed;
        //velocity += creatorVelocity;
        rb.MovePosition((Vector2)transform.position + velocity * Time.deltaTime);

        float dits = Vector2.Distance(oldPos, transform.position);
        if (dits >= deadRad)
        {
            Dead();
        }
    }

    public void SetStartPos(Vector3 pos)
    {
        oldPos = pos;
    }

    public void SetCreatorVelocity(Vector2 vector)
    {
        creatorVelocity = vector;
    }

    public void SetDir(Vector2 dir)
    {
        direction = dir;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void HideObject(bool value)
    {
        gameObject.SetActive(!value);
        rb.isKinematic = value;
    }

    public void SetPool(IObjectPool<Shell> _pool) => pool = _pool;

    void Dead()
    {
        trail.Clear();
        if (!rb.isKinematic)
            pool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<AliveEntity>())
        {
            collision.GetComponent<AliveEntity>().MakeDamage(damage);
            SoundManager.Instance.PlayAudioClip(sound);
            if (collision.GetComponent<Enemy>())
            {
                PopupTextManager.Instance.SpawnDamageText(transform.position, ColorType.enemyDamage, $"-{damage.ToString("0.0")}");
            }
            else
            {
                PopupTextManager.Instance.SpawnDamageText(transform.position, ColorType.playerDamage, $"-{damage.ToString("0.0")}");
            }
            Dead();
        }
    }
}
