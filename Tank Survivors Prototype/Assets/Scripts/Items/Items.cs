using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items : MonoBehaviour
{
    [SerializeField] private float destroyTime = 30;

    protected Player player;

    public virtual void Start()
    {
        player = FindObjectOfType<Player>();
        Destroy(gameObject, destroyTime);
    }

    public virtual void Use()
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Use();
        }
    }
}
