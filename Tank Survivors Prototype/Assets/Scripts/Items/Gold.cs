using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Items
{
    [SerializeField] private float speed;
    [SerializeField] private int maxMoney;

    private Transform counter;

    Camera cam;

    bool fly;

    public override void Start()
    {
        base.Start();
        counter = GameObject.Find("MoneyCounter").transform;
        cam = Camera.main;
    }

    public override void Use()
    {
        GameManager.instance.UpMoney();
        base.Use();
    }

    private void Update()
    {
        if (!fly) return;
        Vector3 worldPos = cam.ScreenToWorldPoint(counter.position);
        transform.position = Vector3.Lerp(transform.position, worldPos, speed * Time.deltaTime);
        Vector3 offset = worldPos - transform.position;
        if (offset.magnitude <= 0.5)
            Use();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            fly = true;
        }
    }
}
