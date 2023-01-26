using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Alive Entity Data", menuName = "Alive Entity Data", order = 63)]
public class AliveEntityData : ScriptableObject
{
    public int maxHealth;
    public int startWeaponId;

    public float moveSpeed = 500;
    public float rotationSpeed = 20;

    public GameObject explosionPrefab;
    public WeaponData weaponData;
}
