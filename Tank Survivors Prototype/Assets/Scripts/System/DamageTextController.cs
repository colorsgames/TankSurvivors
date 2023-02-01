using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Pool;

public class DamageTextController : MonoBehaviour
{
    public static ColorType colorType;

    [SerializeField] private float lifeTime = 2;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float sizeSpeed = 5;

    [SerializeField] Color enemyDamage;
    [SerializeField] Color playerDamage;
    [SerializeField] Color healing;

    [SerializeField] Vector3 maxSize;

    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    [SerializeField] TMP_Text tmp;

    private IObjectPool<DamageTextController> pool;

    Vector2 randPos;

    float currentTime;
    float curretLifeTime;

    private void Start()
    {
        randPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        randPos = (Vector2)transform.position + randPos;
        curretLifeTime = lifeTime;
    }

    private void Update()
    {
        WhaitingForDeath();

        transform.position = Vector2.Lerp(transform.position, randPos, moveSpeed * Time.deltaTime);

        currentTime += Time.deltaTime;
        if (currentTime >= lifeTime / 2)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, sizeSpeed * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxSize, sizeSpeed * Time.deltaTime);
        }
    }

    public void SetDamageText(string text, ColorType type)
    {
        randPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        randPos = (Vector2)transform.position + randPos;

        tmp.text = text;
        if (type == ColorType.enemyDamage)
            tmp.color = enemyDamage;
        else if(type == ColorType.playerDamage)
            tmp.color = playerDamage;
        else
            tmp.color = healing;
    }

    public void HideObject(bool value)
    {
        gameObject.SetActive(!value);
        currentTime = 0;
        transform.localScale = Vector3.zero;
    }

    public void SetPool(IObjectPool<DamageTextController> _pool) => pool = _pool;

    void Dead()
    {
        pool.Release(this);
    }

    void WhaitingForDeath()
    {
        curretLifeTime -= Time.deltaTime;
        if(curretLifeTime <= 0)
        {
            curretLifeTime = lifeTime;
            Dead();
        }
    }
}

public enum ColorType
{
    enemyDamage,
    playerDamage,
    healing
}
