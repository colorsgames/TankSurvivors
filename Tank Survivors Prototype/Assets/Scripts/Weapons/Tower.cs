using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    private Transform parent;
    private Vector2 target;

    public void SetTarget(Vector2 target)
    {
        this.target = target;
    }

    public AliveEntity GetParent()
    {
        return parent.GetComponent<AliveEntity>();
    }

    private void Awake()
    {
        parent = transform.parent;
    }

    private void Start()
    {
        transform.parent = null;
    }

    private void Update()
    {
        Vector2 dir = target - (Vector2)transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
        transform.rotation = rot;
        transform.position = parent.position;
    }
}
