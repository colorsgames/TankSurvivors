using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private float deadRad;
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    private Vector2 creatorVelocity;
    private Vector2 direction;
    private Vector2 velocity;
    private Vector2 oldPos;

    private int damage;

    private void Start()
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

    public void SetCreatorVelocity(Vector2 vector)
    {
        creatorVelocity = vector;
    }

    public void SetDir(Vector2 dir)
    {
        direction = dir;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    void Dead()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<AliveEntity>())
        {
            collision.GetComponent<AliveEntity>().MakeDamage(damage);
            if (collision.GetComponent<Enemy>())
            {
                PopupTextManager.instance.SpawnDamageText(transform.position, ColorType.enemyDamage, $"-{damage}");
            }
            else
            {
                PopupTextManager.instance.SpawnDamageText(transform.position, ColorType.playerDamage, $"-{damage}");
            }
            Dead();
        }
    }
}
