using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoController : MonoBehaviour
{
    [SerializeField] float minAngle, maxAngle;
    [SerializeField] float speed;

    private void Update()
    {
        float angle = Random.Range(minAngle, maxAngle);
        transform.localRotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
